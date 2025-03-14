using GHOST.TalentosCortes.Domain.Core.Notifications;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Events;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Repository;
using GHOST.TalentosCortes.Domain.Entities.CommandHandlers;
using GHOST.TalentosCortes.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Commands
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<RegistrarClienteCommand>,
        IRequestHandler<AtualizarClienteCommand>,
        IRequestHandler<ExcluirClienteCommand>,
        IRequestHandler<IncluirEnderecoClienteCommand>,
        IRequestHandler<AtualizarEnderecoClienteCommand>
    {
       private readonly IClienteRepository _clienteRepository;
       private readonly IUser _user;
       private readonly IMediatorHandler _mediator;

        public ClienteCommandHandler(IClienteRepository clienteRepository,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications,
            IUser user,
            IMediatorHandler mediator) : base(uow, mediator, notifications)
        {
            _clienteRepository = clienteRepository;
            _user = user;
            _mediator = mediator;
        }

        public Task Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            var endereco = new Endereco(
                message.Endereco.Id,
                message.Endereco.Logradouro,
                message.Endereco.Numero,
                message.Endereco.Complemento,
                message.Endereco.Bairro,
                message.Endereco.CEP,
                message.Endereco.Cidade,
                message.Endereco.Estado,
                message.Endereco.ClienteId.Value);

            var cliente = Cliente.ClienteFactory.NovoClienteCompleto(   
                message.Id,
                message.NomeCompleto,
                message.CPF,
                message.RG,
                message.TelResidencial,
                message.TelCelular,
                message.Email,
                message.RedeSocial,
                message.Data,
                message.CheckEndereco,
                endereco,
                message.MasterUserId,
                message.GeneroId

            );

            if (!ClienteValido(cliente)) return Task.CompletedTask;

            _clienteRepository.Add(cliente);

            if (Commit())
            {
                _mediator.PublishEvent(new ClienteRegistradoEvent(
                    message.Id,
                    message.NomeCompleto,
                    message.CPF,
                    message.RG,
                    message.TelResidencial,
                    message.TelCelular,
                    message.Email,
                    message.RedeSocial,
                    message.Data,
                    message.CheckEndereco        
                ));
            }

            return Task.CompletedTask;
        }


        public Task Handle(AtualizarClienteCommand message, CancellationToken cancellationToken)
        {
            var clienteAtual = _clienteRepository.GetById(message.Id);

            if (!ClienteExistente(message.Id, message.MessageType)) return Task.CompletedTask;

            if (clienteAtual.MasterUserId != _user.GetUserId())
            {
                _mediator.PublishEvent(new DomainNotification(message.MessageType,"Cliente não pertencente ao Master User logado."));
                return Task.CompletedTask;
            }

            var cliente = Cliente.ClienteFactory.NovoClienteCompleto(
                message.Id,
                message.NomeCompleto,
                message.CPF,
                message.RG,
                message.TelResidencial,
                message.TelCelular,
                message.Email,
                message.RedeSocial,
                message.Data,
                message.CheckEndereco,
                clienteAtual.Endereco,
                message.MasterUserId,
                message.GeneroId

            );

            //if (!cliente.CheckEndereco && cliente.Endereco == null)
            //{
            //    _mediator.PublishEvent(new DomainNotification(message.MessageType, "Não é possivel atualizar um cliente sem informar o endereço"));
            //    return Task.CompletedTask;
            //}

            if (!ClienteValido(cliente)) return Task.CompletedTask;

            _clienteRepository.Update(cliente);

            if (Commit())
            {
                _mediator.PublishEvent(new ClienteAtualizadoEvent(
                    message.Id,
                    message.NomeCompleto,
                    message.CPF,
                    message.RG,
                    message.TelResidencial,
                    message.TelCelular,
                    message.Email,
                    message.RedeSocial,
                    message.Data,
                    message.CheckEndereco
                ));

            }

            return Task.CompletedTask;
        }

        public Task Handle(ExcluirClienteCommand message, CancellationToken cancellationToken)
        {
            if (!ClienteExistente(message.Id, message.MessageType)) return Task.CompletedTask;

            var clienteAtual = _clienteRepository.GetById(message.Id);

            if (clienteAtual.MasterUserId != _user.GetUserId())
            {
                _mediator.PublishEvent(new DomainNotification(message.MessageType, "Cliente não pertencente ao MasterUser logado."));
                return Task.CompletedTask;
            }

            // Validacoes de negocio
            clienteAtual.ExcluirClientes();

            _clienteRepository.Update(clienteAtual);

            if (Commit())
            {
                _mediator.PublishEvent(new ClienteExcluidoEvent(message.Id));
            }

            return Task.CompletedTask;
        }
    

       private bool ClienteValido(Cliente cliente)
        {
            if (cliente.EhValido()) return true;

            NotificarValidacoesErro(cliente.ValidationResult);
            return false;
        }

       private bool ClienteExistente(Guid id, string messageType)
        {
            var ligacao = _clienteRepository.GetById(id);

            if (ligacao != null) return true;

            _mediator.PublishEvent(new DomainNotification(messageType, "Cliente não encontrado."));
            return false;
        }

       public Task Handle(IncluirEnderecoClienteCommand message, CancellationToken cancellationToken)
       {
           var endereco = new Endereco(message.Id, message.Logradouro, message.Numero, message.Complemento, message.Bairro, message.CEP, message.Cidade, message.Estado, message.ClienteId.Value);
           if (!endereco.EhValido())
           {
               NotificarValidacoesErro(endereco.ValidationResult);
               return Task.CompletedTask;
           }

           var cliente = _clienteRepository.GetById(message.ClienteId.Value);
           cliente.EnderecoCliente();

           _clienteRepository.Update(cliente);
           _clienteRepository.AdicionarEndereco(endereco);

           if (Commit())
           {
               _mediator.PublishEvent(new EnderecoEventoAdicionadoEvent(endereco.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.CEP, endereco.Cidade, endereco.Estado, endereco.ClienteId.Value));
           }

           return Task.CompletedTask;
       }

       public Task Handle(AtualizarEnderecoClienteCommand message, CancellationToken cancellationToken)
       {
           var endereco = new Endereco(message.Id, message.Logradouro, message.Numero, message.Complemento, message.Bairro, message.CEP, message.Cidade, message.Estado, message.ClienteId.Value);

            if (!endereco.EhValido())
           {
               NotificarValidacoesErro(endereco.ValidationResult);
               return Task.CompletedTask;
           }

           _clienteRepository.AtualizarEndereco(endereco);

           if (Commit())
           {
               _mediator.PublishEvent(new EnderecoEventoAtualizadoEvent(endereco.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.CEP, endereco.Cidade, endereco.Estado, endereco.ClienteId.Value));
           }

           return Task.CompletedTask;
       }
    }
}