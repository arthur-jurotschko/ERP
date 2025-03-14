using System;
using System.Collections.Generic;
using AutoMapper;
using GHOST.TalentosCortes.Domain.Core.Notifications;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Commands;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Repository;
using GHOST.TalentosCortes.Domain.Interfaces;
using GHOST.TalentosCortes.Services.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GHOST.TalentosCortes.Services.API.Controllers
{
    public class ClientesController : BaseController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public ClientesController(INotificationHandler<DomainNotification> notifications,
                                 IUser user,
                                 IClienteRepository clienteRepository,
                                 IMapper mapper,
                                 IMediatorHandler mediator) : base(notifications, user, mediator)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getdashboard")]
        //[Authorize(Policy = "PodeGravar")]
        public IEnumerable<DashboardViewModel> GetDashboard()
        {
            return _mapper.Map<IEnumerable<DashboardViewModel>>(_clienteRepository.Getdashboard());
        }

        [HttpPost]
        [Route("adicionar-endereco")]
        [Authorize(Policy = "PodeGravar")]
        public IActionResult Post([FromBody]EnderecoViewModel enderecoViewModel)
        {
            if (!ModelStateValida())
            {
                return Response();
            }

            var eventoCommand = _mapper.Map<IncluirEnderecoClienteCommand>(enderecoViewModel);

            _mediator.SubmitCommand(eventoCommand);
            return Response(eventoCommand);
        }

        [HttpPut]
        [Route("atualizar-endereco")]
        [Authorize(Policy = "PodeGravar")]
        public IActionResult Put([FromBody]EnderecoViewModel enderecoViewModel)
        {
            if (!ModelStateValida())
            {
                return Response();
            }

            var eventoCommand = _mapper.Map<AtualizarEnderecoClienteCommand>(enderecoViewModel);

            _mediator.SubmitCommand(eventoCommand);
            return Response(eventoCommand);
        }

        [HttpGet]
        [Route("obterclientes")]
        [Authorize(Policy = "PodeGravar")]
        //[Authorize]
        public IEnumerable<ClienteViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<ClienteViewModel>>(_clienteRepository.GetAll());
        }

        [HttpGet]
        //[Authorize(Policy = "PodeGravar")]
        [Route("cliente-id/{id:guid}")]
        public ClienteViewModel Get(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(_clienteRepository.GetById(id));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("genero")]
        public IEnumerable<GeneroViewModel> ObterGenero()
        {
            return _mapper.Map<IEnumerable<GeneroViewModel>>(_clienteRepository.ObterGenero());
        }

        [HttpGet]
        [Authorize(Policy = "PodeGravar")]
        [Route("meus-clientes")]
        public IEnumerable<ClienteViewModel> ObterMeusClientes()
        {
            return _mapper.Map<IEnumerable<ClienteViewModel>>(_clienteRepository.ObterClientesPorMasterUser(MasterUserId));
        }

        [HttpPost]
        [Route("novo-cliente")]
        [Authorize(Policy = "PodeGravar")]
        public IActionResult Post([FromBody]ClienteViewModel clienteViewModel)
        {
            if (!ModelStateValida())
            {
                return Response();
            }

            var eventoCommand = _mapper.Map<RegistrarClienteCommand>(clienteViewModel);

            _mediator.SubmitCommand(eventoCommand);
            return Response(eventoCommand);
        }

        [HttpGet]
        //[Authorize(Policy = "PodeGravar")]
        [Route("adicionarClienteDashboard")]
        public IEnumerable<DashboardViewModel> AddClienteDashboard()
        {
            return _mapper.Map<IEnumerable<DashboardViewModel>>(_clienteRepository.AdicionarClienteDashboard());
            
        }

        [HttpPut]
        [Route("atualizar-cliente")]
        [Authorize(Policy = "PodeGravar")]
        public IActionResult Put([FromBody]ClienteViewModel clienteViewModel)
        {
            if (!ModelStateValida())
            {
                return Response();
            }

            var eventoCommand = _mapper.Map<AtualizarClienteCommand>(clienteViewModel);

            _mediator.SubmitCommand(eventoCommand);
            return Response(eventoCommand);
        }

        [HttpDelete]
        [Route("excluir-cliente/{id:guid}")]
        //[Authorize(Policy = "PodeGravar")]
        public IActionResult Delete(Guid id)
        {
            var clienteViewModel = new ClienteViewModel { Id = id };
            var eventoCommand = _mapper.Map<ExcluirClienteCommand>(clienteViewModel);

            _mediator.SubmitCommand(eventoCommand);
            return Response(eventoCommand);
        }

        private bool ModelStateValida()
        {
            if (ModelState.IsValid) return true;

            NotificarErroModelInvalida();
            return false;
        }

    }
}

//[HttpGet]
//[Route("getdashboard")]
////[Authorize(Policy = "PodeGravar")]
////[Authorize]
//public IEnumerable<dashboardViewModel> GetDashboard()
//{
//    return _mapper.Map<IEnumerable<dashboardViewModel>>(_clienteRepository.Getdashboard());
//}

//[HttpGet]
////[Authorize(Policy = "PodeGravar")]
//[Route("tipo-contato")]
//public IEnumerable<TipoContatoViewModel> ObterTipoContato()
//{
//    return _mapper.Map<IEnumerable<TipoContatoViewModel>>(_ligacaoRepository.ObterTiposContato());
//}

//[HttpGet]
//[AllowAnonymous]
//[Route("ligacoes/enviar-email")]
//public IEnumerable<EnviarEmailViewModel> ObterEnviarEmail()
//{
//    return _mapper.Map<IEnumerable<EnviarEmailViewModel>>(_ligacaoRepository.ObterEnvioEmails());
//}

//[HttpGet]
//[AllowAnonymous]
//[Route("ligacoes/foram-atendidos")]
//public IEnumerable<FoiAtendidoViewModel> ObterFoiAtendido()
//{
//    return _mapper.Map<IEnumerable<FoiAtendidoViewModel>>(_ligacaoRepository.ObterAtendidos());
//}

//[HttpGet]
//[AllowAnonymous]
//[Route("ligacoes/motivo-recusa")]
//public IEnumerable<MotivoRecusaViewModel> ObterMotivoRecusa()
//{
//    return _mapper.Map<IEnumerable<MotivoRecusaViewModel>>(_ligacaoRepository.ObterMotivosRecusa());
//}

//[HttpGet]
//[AllowAnonymous]
//[Route("ligacoes/retornos")]
//public IEnumerable<RetornoViewModel> ObterRetornos()
//{
//    return _mapper.Map<IEnumerable<RetornoViewModel>>(_ligacaoRepository.ObterRetorno());
//}


//[HttpGet]
//[Authorize(Policy = "PodeLerEventos")]
//[Route("eventos/meus-eventos")]
//public IEnumerable<EventoViewModel> ObterMeusEventos()
//{
//    return _mapper.Map<IEnumerable<EventoViewModel>>(_eventoRepository.ObterEventoPorOrganizador(OrganizadorId));
//}


