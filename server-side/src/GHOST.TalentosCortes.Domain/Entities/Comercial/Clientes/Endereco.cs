using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using GHOST.TalentosCortes.Domain.Core.Models;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes
{
    public class Endereco : Entity<Endereco>
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid? ClienteId { get; private set; }

        // EF Propriedade de Navegação
        public virtual Cliente Cliente { get; private set; }


        public Endereco(Guid id, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid? clienteId)
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

        // Construtor para o EF
        protected Endereco() { }

        public override bool EhValido()
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage("O Logradouro precisa ser fornecido")
                .Length(2, 150).WithMessage("O Logradouro precisa ter entre 2 e 150 caracteres");

            RuleFor(c => c.Bairro)
                .NotEmpty().WithMessage("O Bairro precisa ser fornecido")
                .Length(2, 150).WithMessage("O Bairro precisa ter entre 2 e 150 caracteres");

            RuleFor(c => c.CEP)
                .NotEmpty().WithMessage("O CEP precisa ser fornecido")
                .Length(8).WithMessage("O CEP precisa ter 8 caracteres");

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("A Cidade precisa ser fornecida")
                .Length(2, 150).WithMessage("A Cidade precisa ter entre 2 e 150 caracteres");

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage("O Estado precisa ser fornecido")
                .Length(2, 150).WithMessage("O Estado precisa ter entre 2 e 150 caracteres");

            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage("O Numero precisa ser fornecido")
                .Length(1, 10).WithMessage("O Numero precisa ter entre 1 e 10 caracteres");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}