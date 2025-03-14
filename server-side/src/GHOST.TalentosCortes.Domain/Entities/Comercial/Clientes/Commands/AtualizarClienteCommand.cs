using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Commands
{
   public class AtualizarClienteCommand : BaseClienteCommand
    {
        public AtualizarClienteCommand
        (
            Guid id,
            string nomecompleto,
            string cpf,
            string rg,
            string telresidencial,
            string telcelular,
            string email,
            string redesocial,
            DateTime data,
            bool checkendereco,
            Guid? masterUserId,
            Guid generoId
        )
        {
            Id = id;
            NomeCompleto = nomecompleto;
            CPF = cpf;
            RG = rg;
            TelResidencial = telresidencial;
            TelCelular = telcelular;
            Email = email;
            RedeSocial = redesocial;
            Data = data;
            CheckEndereco = checkendereco;
            GeneroId = generoId;
        }


    }
}
