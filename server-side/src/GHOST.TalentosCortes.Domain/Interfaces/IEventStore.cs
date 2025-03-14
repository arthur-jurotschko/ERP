using GHOST.TalentosCortes.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Domain.Interfaces
{
    public interface IEventStore
    {
        void SaveEvent<T>(T evento) where T : Event;
    }
}