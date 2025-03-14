using System;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Events
{
    public class ClienteExcluidoEvent : BaseClienteEvent
    {
        public ClienteExcluidoEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
