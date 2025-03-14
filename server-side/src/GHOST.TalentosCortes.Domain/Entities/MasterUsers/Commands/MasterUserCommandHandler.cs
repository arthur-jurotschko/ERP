using GHOST.TalentosCortes.Domain.Core.Notifications;
using GHOST.TalentosCortes.Domain.Entities.CommandHandlers;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers.Events;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers.Repository;
using GHOST.TalentosCortes.Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GHOST.TalentosCortes.Domain.Entities.MasterUsers.Commands
{
    public class MasterUserCommandHandler : CommandHandler,
        IRequestHandler<RegistrarMasterUserCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IMasterUserRepository _masterUserRepository;

        public MasterUserCommandHandler(
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications,
            IMasterUserRepository masterUserRepository, IMediatorHandler mediator) : base(uow, mediator, notifications)
        {
            _masterUserRepository = masterUserRepository;
            _mediator = mediator;
        }

        public Task Handle(RegistrarMasterUserCommand message, CancellationToken cancellationToken)
        {
            var masterUser = new MasterUser(message.Id, message.Nome, message.CPF, message.Email);

            if (!masterUser.EhValido())
            {
                NotificarValidacoesErro(masterUser.ValidationResult);
                return Task.CompletedTask;
            }

            var masterUserExistente = _masterUserRepository.Find(o => o.CPF == masterUser.CPF || o.Email == masterUser.Email);

            if (masterUserExistente.Any())
            {
                _mediator.PublishEvent(new DomainNotification(message.MessageType, "CPF ou e-mail já utilizados"));
            }

            _masterUserRepository.Add(masterUser);

            if (Commit())
            {
                _mediator.PublishEvent(new MasterUserRegistradoEvent(masterUser.Id, masterUser.Nome, masterUser.CPF, masterUser.Email));
            }

            return Task.CompletedTask;
        }

    }
}
