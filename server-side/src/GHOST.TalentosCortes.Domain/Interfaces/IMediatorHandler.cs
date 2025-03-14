using GHOST.TalentosCortes.Domain.Core.Commands;
using GHOST.TalentosCortes.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GHOST.TalentosCortes.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task SubmitCommand<T>(T comando) where T : Command;
    }
}