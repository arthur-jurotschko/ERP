using GHOST.TalentosCortes.Domain.Core.Commands;
using System;

namespace GHOST.TalentosCortes.Domain.Entities.MasterUsers.Commands
{
    public class RegistrarMasterUserCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }

        public RegistrarMasterUserCommand(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }
    }
}
