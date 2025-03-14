using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GHOST.TalentosCortes.Domain.Core.Notifications;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers.Commands;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers.Repository;
using GHOST.TalentosCortes.Domain.Interfaces;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Authorization;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Models;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Models.AccountViewModels;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Services;
using GHOST.TalentosCortes.Services.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace GHOST.TalentosCortes.Services.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IMasterUserRepository _masterUserRepository;
        private readonly IMediatorHandler _mediator;
        private readonly TokenDescriptor _tokenDescriptor;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;


        public AccountController(
                    UserManager<ApplicationUser> userManager,
                    SignInManager<ApplicationUser> signInManager,
                    ILoggerFactory loggerFactory,
                    TokenDescriptor tokenDescriptor,
                    INotificationHandler<DomainNotification> notifications,
                    IUser user,
                    IEmailSender emailSender,
                    IMapper mapper,
                    IMasterUserRepository masterUserRepository,
                    IMediatorHandler mediator) : base(notifications, user, mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _masterUserRepository = masterUserRepository;
            _mediator = mediator;
            _mapper = mapper;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _tokenDescriptor = tokenDescriptor;
        }

        private static long ToUnixEpochDate(DateTime date)
      => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);



        [HttpPost]
        [AllowAnonymous]
        //  [ValidateAntiForgeryToken]
        [Route("novo-estagiario")]
        public async Task<IActionResult> RegisterEstagiario([FromBody] RegisterViewModel model, int version)
        {

            if (version == 2)
            {
                return Response(new { Message = "API V2 não disponível" });
            }

            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response();
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("Estagiario", "Ler"));
                await _userManager.AddClaimAsync(user, new Claim("Estagiario", "Gravar"));


                var registroCommand = new RegistrarMasterUserCommand(Guid.Parse(user.Id), model.Nome, model.CPF, user.Email);
                await _mediator.SubmitCommand(registroCommand);

                if (!OperacaoValida())
                {
                    await _userManager.DeleteAsync(user);
                    return Response(model);
                }

                _logger.LogInformation(1, "Usuario criado com sucesso!");
                var response = GerarTokenUsuario(new LoginViewModel { Email = model.Email, Senha = model.Senha });
                return Response(response);
                // return Response(model);
            }
            AdicionarErrosIdentity(result);
            return Response(model);
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("nova-conta")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model, int version)
        {

            if (version == 2)
            {
                return Response(new { Message = "API V2 não disponível" });
            }

            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response();
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("Administrador", "Ler"));
                await _userManager.AddClaimAsync(user, new Claim("Administrador", "Gravar"));


                var registroCommand = new RegistrarMasterUserCommand(Guid.Parse(user.Id), model.Nome, model.CPF, user.Email);
                await _mediator.SubmitCommand(registroCommand);

                if (!OperacaoValida())
                {
                    await _userManager.DeleteAsync(user);
                    return Response(model);
                }

                _logger.LogInformation(1, "Usuario criado com sucesso!");
                var response = GerarTokenUsuario(new LoginViewModel { Email = model.Email, Senha = model.Senha });
                return Response(response);
               
            }
            AdicionarErrosIdentity(result);
            return Response(model);
        }


        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    NotificarErro(user.ToString(), "O endereço de e-mail ou a senha que você inseriu não são válidos. Tente novamente ou entre em contato com um administrador.");
                    return Response(model);
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   $"Please reset your password by clicking here: <a href='http://localhost:4200/resetpassword'> http://localhost:4200/resetpassword  </a></br>" +
                   $"Seu código de redefinição de senha: { code }");

            }

            return Response(model);
        }


        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                NotificarErro(user.ToString(), "Usuario não existe.");
                return Response(model);
            }
            var emailConfirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                var _user = _userManager.UpdateAsync(user);

                await _emailSender.SendEmailAsync(model.Email, "Senha Alterada com sucesso",
                $"Sua senha foi alterada com sucesso.");

                _logger.LogInformation("Senha Alterada com sucesso.");

                return Response(result);
            }

            

            NotificarErro(result.ToString(), "Houve erros ao alterar a senha do usuário.");
            return Response(model);
        }


        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ForgotPasswordViewModel model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                _logger.LogInformation($"Não é possível carregar o usuário com o ID'{user}'. ");
            }

            var code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            var result = await _userManager.ConfirmEmailAsync(user, code);

            await _emailSender.SendEmailAsync(model.Email, "Email confirmado com sucesso.",
                    $"volte para página inicial: <a href='http://localhost:4200'>Click aqui</a>");       

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Obrigado por confirmar seu email.");
            }

            return Response(result);
        }



        [HttpGet]
        [AllowAnonymous]
        [Route("GetAll-Users")]
        public IActionResult GetAll()
        {
            var users = _masterUserRepository.GetAll();
            var user = _mapper.Map<IEnumerable<MasterUserViewModel>>(_masterUserRepository.GetAll());

            return Ok(user);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("conta")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Usuario logado com sucesso");
                var response = GerarTokenUsuario(model);
                return Response(response);
            }

            NotificarErro(result.ToString(), "Falha ao realizar o login");
            return Response(model);
        }

        private async Task<object> GerarTokenUsuario(LoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            // Necessário converver para IdentityClaims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenDescriptor.Issuer,
                Audience = _tokenDescriptor.Audience,
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
            });

            var encodedJwt = handler.WriteToken(securityToken);
            var orgUser = _masterUserRepository.GetById(Guid.Parse(user.Id));

            var response = new
            {
                access_token = encodedJwt,
                expires_in = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),
                user = new
                {
                    id = user.Id,
                    nome = orgUser.Nome,
                    email = orgUser.Email,
                    claims = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;
        }
  

private bool ModelStateValida()
        {
            if (ModelState.IsValid) return true;

            NotificarErroModelInvalida();
            return false;
        }
    }
}