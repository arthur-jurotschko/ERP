using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using GHOST.TalentosCortes.Domain.Core.Models;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes
{
   public class Genero : Entity<Genero>
    {
       public Genero(Guid id)
       {
           Id = id;
       }

       public string Nome { get; private set; }

       // EF Propriedade de Navegação
       public virtual ICollection<Cliente> Clientes { get; set; }

       // Construtor para o EF
       protected Genero () { }

       public override bool EhValido()
       {
           return true;
       }
   }
}