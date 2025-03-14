using GHOST.TalentosCortes.Domain.Core.Models;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Domain.Entities.MasterUsers
{
    public class MasterUser : Entity<MasterUser>
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }
        

        public MasterUser(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }

        // EF Construtor
        protected MasterUser() { }

        // EF Propriedade de Navegação
        public virtual ICollection<Cliente> Clientes { get; set; }

        public override bool EhValido()
        {
            return true;
        }

      
    }
}
