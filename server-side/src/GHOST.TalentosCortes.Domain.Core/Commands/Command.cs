using GHOST.TalentosCortes.Domain.Core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Domain.Core.Commands
{
    public class Command : Message, IRequest
    {
        public DateTime Timestamp { get; set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
