using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event evento, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = evento.AggregateId;
            MessageType = evento.MessageType;
            Data = data;
            User = user;
        }

        // EF Constructor
        protected StoredEvent() { }

        public Guid Id { get; set; }

        public string Data { get; set; }

        public string User { get; set; }
    }
}