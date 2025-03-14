using GHOST.TalentosCortes.Domain.Core.Events;
using System;

namespace GHOST.TalentosCortes.Domain.Entities.MasterUsers.Events
{
    public class MasterUserRegistradoEvent : Event
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }

        public MasterUserRegistradoEvent(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }
    }
}