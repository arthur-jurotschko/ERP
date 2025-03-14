using GHOST.TalentosCortes.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Infrastructured.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}
