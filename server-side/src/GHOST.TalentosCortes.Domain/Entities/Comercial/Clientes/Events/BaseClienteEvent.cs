using GHOST.TalentosCortes.Domain.Core.Events;
using System;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Events
{
    public abstract class BaseClienteEvent : Event
    {
        public Guid Id { get; protected set; }
        public string NomeCompleto { get; protected set; }
        public string CPF { get; protected set; }
        public string RG { get; protected set; }
        public string TelResidencial { get; protected set; }
        public string TelCelular { get; protected set; }
        public string Email { get; protected set; }
        public string RedeSocial { get; protected set; }
        public DateTime Data { get; protected set; }
        public bool CheckEndereco { get; protected set; }

    }
}
