using GHOST.TalentosCortes.Domain.Core.Commands;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Commands
{
   public class BaseClienteCommand : Command
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
        public bool Excluido { get; protected set; }
        public Guid MasterUserId { get; protected set; }
        public Guid GeneroId { get; protected set; }
    }
}
