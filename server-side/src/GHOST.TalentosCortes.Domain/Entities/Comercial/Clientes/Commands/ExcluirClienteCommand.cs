using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Commands
{
   public class ExcluirClienteCommand : BaseClienteCommand
    {
        public ExcluirClienteCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }
    }
}
