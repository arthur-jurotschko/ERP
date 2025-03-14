using GHOST.TalentosCortes.Domain.Core.Commands;
using System;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Commands
{
    public class IncluirEnderecoClienteCommand : Command
    {
        public IncluirEnderecoClienteCommand(Guid id, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid? clienteId)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            ClienteId = clienteId;
        }

        public Guid Id { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid? ClienteId { get; private set; }
    }
}