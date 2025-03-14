using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GHOST.TalentosCortes.Domain.Entities.MasterUsers.Events
{
    public class MasterUserEventHandler :
        INotificationHandler<MasterUserRegistradoEvent>
    {
        public Task Handle(MasterUserRegistradoEvent message, CancellationToken cancellationToken)
        {
            // TODO: Enviar um email?
            return Task.CompletedTask;
        }
    }
}
