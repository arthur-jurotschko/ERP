using GHOST.TalentosCortes.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        IEnumerable<Cliente> ObterClientesPorMasterUser(Guid masterUserId);
        Endereco ObterEnderecoPorId(Guid id);
        void AdicionarEndereco(Endereco endereco);
        void AtualizarEndereco(Endereco endereco);
        IEnumerable<Genero> ObterGenero();
        Cliente ObterMeusClientesPorId(Guid id, Guid masterUserId);
        IEnumerable<Dashboard> Getdashboard();
        IEnumerable<Dashboard> AdicionarClienteDashboard();
     
    }
}
