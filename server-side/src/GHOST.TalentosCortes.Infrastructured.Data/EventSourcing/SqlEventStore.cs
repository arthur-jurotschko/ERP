using GHOST.TalentosCortes.Domain.Core.Events;
using GHOST.TalentosCortes.Domain.Interfaces;
using GHOST.TalentosCortes.Infrastructured.Data.Repository.EventSourcing;
using Newtonsoft.Json;


namespace GHOST.TalentosCortes.Infrastructured.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        readonly IEventStoreRepository _eventStoreRepository;
        readonly IUser _user;

        public SqlEventStore(IEventStoreRepository eventStoreRepository, IUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public void SaveEvent<T>(T evento) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(evento);

            var storedEvent = new StoredEvent(
                evento,
                serializedData,
                _user.GetUserId().ToString());

            _eventStoreRepository.Store(storedEvent);
        }
    }
}