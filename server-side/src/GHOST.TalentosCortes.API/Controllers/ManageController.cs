using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GHOST.TalentosCortes.Domain.Core.Notifications;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers.Repository;
using GHOST.TalentosCortes.Domain.Interfaces;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Authorization;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Models;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Models.ManageViewModels;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GHOST.TalentosCortes.Services.API.Controllers
{
    [Produces("application/json")]

    public class ManageController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IMasterUserRepository _masterUserRepository;
        private readonly IMediatorHandler _mediator;
        private readonly TokenDescriptor _tokenDescriptor;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ISmsSender _smsSender;

        public ManageController(
                    UserManager<ApplicationUser> userManager,
                    SignInManager<ApplicationUser> signInManager,
                    ILoggerFactory loggerFactory,
                    TokenDescriptor tokenDescriptor,
                    INotificationHandler<DomainNotification> notifications,
                    IUser user,
                    IEmailSender emailSender,
                    ISmsSender smsSender,
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
            _smsSender = smsSender;

        }

        [TempData]
        public string StatusMessage { get; set; }



        [HttpPost]
        [AllowAnonymous]
        [Route("PostAddPhoneNumber")]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }
            // Generate the token and send it
            var user = await _userManager.GetUserAsync(User);
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);
            await _smsSender.SendSmsAsync(model.PhoneNumber, "Your security code is: " + code);
            return Response(model);
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [AllowAnonymous]
        [Route("EnableTwoFactorAuthentication")]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(1, "User enabled two-factor authentication.");
            }
            return Response(user);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("DisableTwoFactorAuthentication")]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(2, "User disabled two-factor authentication.");
            }
            return Response(user);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("PostVerifyPhoneNumber")]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var result = await _userManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Code);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Response(result);
                }
            }
            // If we got this far, something failed, redisplay the form
            ModelState.AddModelError(string.Empty, "Failed to verify phone number");
            return Response(model);

        }


        [HttpPost]
        [AllowAnonymous]
        [Route("RemovePhoneNumber")]
        public async Task<IActionResult> RemovePhoneNumber()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var result = await _userManager.SetPhoneNumberAsync(user, null);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Response(result);
                }
            }
            return Response(user);
        }


        //[HttpGet]
        //[Route("GetChangePassword")]
        //public async Task<IActionResult> ChangePassword()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    var hasPassword = await _userManager.HasPasswordAsync(user);
        //    if (!hasPassword)
        //    {
        //        _logger.LogInformation("Usuario não existe");
        //        return Response(hasPassword);
        //    }

        //    var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
        //    return Response(model);
        //}


        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        [Route("PostChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, FindEmailViewModel find)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            var user = await _userManager.FindByEmailAsync(find.Email);
            if (user == null)
            {
                _logger.LogInformation("Usuario não existe");
                return Response(user);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {

                return Response(result);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation(1, "User changed their password successfully.");

            //NotificarErro(result.ToString(), "Senha do usuário alterada com sucesso.");

            return Response(model);

        }



        #region Helpers



        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        //private string GenerateQrCodeUri(string email, string unformattedKey)
        //{
        //    return string.Format(
        //        AuthenticatorUriFormat,
        //        _urlEncoder.Encode("WebApplication1"),
        //        _urlEncoder.Encode(email),
        //        unformattedKey);
        //}

        private async Task LoadSharedKeyAndQrCodeUriAsync(ApplicationUser user, EnableAuthenticatorViewModel model)
        {
            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            model.SharedKey = FormatKey(unformattedKey);
            //  model.AuthenticatorUri = GenerateQrCodeUri(user.Email, unformattedKey);
        }

    }
}


