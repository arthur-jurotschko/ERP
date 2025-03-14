using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Events
{
    public class ClienteEventHandler :
        INotificationHandler<ClienteRegistradoEvent>,
        INotificationHandler<ClienteAtualizadoEvent>,
        INotificationHandler<ClienteExcluidoEvent>,
        INotificationHandler<EnderecoEventoAdicionadoEvent>,
        INotificationHandler<EnderecoEventoAtualizadoEvent>
    {

       public Task Handle(ClienteRegistradoEvent message, CancellationToken cancellationToken)
       {
           // TODO: Disparar alguma ação
           return Task.CompletedTask;
       }

       public Task Handle(ClienteAtualizadoEvent message, CancellationToken cancellationToken)
       {
           // TODO: Disparar alguma ação
           return Task.CompletedTask;
       }

       public Task Handle(ClienteExcluidoEvent message, CancellationToken cancellationToken)
       {
           // TODO: Disparar alguma ação
           return Task.CompletedTask;
       }

        public Task Handle(EnderecoEventoAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Disparar alguma ação
            return Task.CompletedTask;
        }

        public Task Handle(EnderecoEventoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Disparar alguma ação
            return Task.CompletedTask;
        }
    }
}