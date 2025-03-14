using GHOST.TalentosCortes.Domain.Core.Events;
using System;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Events
{
    public class EnderecoEventoAtualizadoEvent : Event
    {
        public Guid Id { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public EnderecoEventoAtualizadoEvent(Guid enderecoId, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid clienteId)
        {
            Id = enderecoId;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            AggregateId = clienteId;
        }
    }
}