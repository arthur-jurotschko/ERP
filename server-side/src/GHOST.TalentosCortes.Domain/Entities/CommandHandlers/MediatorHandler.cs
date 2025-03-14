using GHOST.TalentosCortes.Domain.Core.Commands;
using GHOST.TalentosCortes.Domain.Core.Events;
using GHOST.TalentosCortes.Domain.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace GHOST.TalentosCortes.Domain.Entities.CommandHandlers
{
    public class MediatorHandler : IMediatorHandler
    {
        readonly IMediator _mediator;
        readonly IEventStore _eventStore;

        public MediatorHandler(IMediator mediator, IEventStore eventStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
        }

        public async Task SubmitCommand<T>(T comando) where T : Command
        {
            await _mediator.Send(comando);
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            if (!evento.MessageType.Equals("DomainNotification"))
                _eventStore?.SaveEvent(evento);

            await _mediator.Publish(evento);
        }
    }
}
