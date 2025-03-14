using System;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Events
{
    public class ClienteRegistradoEvent : BaseClienteEvent
    {
        public ClienteRegistradoEvent(
            Guid id,
            string nomecompleto,
            string cpf,
            string rg,
            string telresidencial,
            string telcelular,
            string email,
            string redesocial,
            DateTime data,
            bool checkendereco        
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
            
            AggregateId = id;
        }
    }
}
