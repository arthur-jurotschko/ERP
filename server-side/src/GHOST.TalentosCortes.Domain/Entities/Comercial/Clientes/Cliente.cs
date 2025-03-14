using FluentValidation;
using GHOST.TalentosCortes.Domain.Core.Models;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes
{
    public class Cliente : Entity <Cliente>
    {

        public Cliente
        (
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
            Id = Guid.NewGuid();
            NomeCompleto = nomecompleto;
            CPF = cpf;
            RG = rg;
            TelResidencial = telresidencial;
            TelCelular = telcelular;
            Email = email;
            RedeSocial = redesocial;
            Data = data;
            CheckEndereco = checkendereco;
        }

        private Cliente() { }

        //Raiz de Agregação
        public string NomeCompleto { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }
        public string TelResidencial { get; private set; }
        public string TelCelular { get; private set; }
        public string Email { get; private set; }
        public string RedeSocial { get; private set; }
        public DateTime Data { get; private set; }
        public bool Excluido { get; private set; }
        public bool CheckEndereco { get; private set; }

        public Guid MasterUserId { get; private set; }
        public Guid? EnderecoId { get; private set; }
        public Guid? GeneroId { get; private set; }

        // EF propriedades de navegacao
        public virtual Endereco Endereco { get; private set; }
        public virtual Genero Genero { get; private set; }
        public virtual MasterUser MasterUser { get; private set; }

        public void AtribuirEndereco(Endereco endereco)
        {
            if (!endereco.EhValido()) return;
            Endereco = endereco;
        }

        public void AtribuirGenero(Genero genero)
        {
            if (!genero.EhValido()) return;
            Genero = genero;
        }

        public void ExcluirClientes()
        {
            // TODO: Deve validar alguma regra?
            Excluido = true;
        }

        public void EnderecoCliente()
        {
            // Alguma validacao de negocio?
            CheckEndereco = false;
        }

        private void Validar()
        {
           // ValidarEndereco();
            ValidarCliente();
           // ValidarLocal();
            ValidationResult = Validate(this);
        }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        #region Validação de Fórmulas

        //private void ValidarEndereco()
        //{
        //    if (!CheckEndereco) return;
        //    if (Endereco.EhValido()) return;

        //    foreach (var error in Endereco.ValidationResult.Errors)
        //    {
        //        ValidationResult.Errors.Add(error);
        //    }
        //}

        #endregion

        #region Validação de Campo

        private void ValidarCliente()
        {
            RuleFor(c => c.NomeCompleto)
                .NotEmpty().WithMessage("O nome do cliente precisa ser fornecido.")
                .Length(2, 150).WithMessage("O nome do cliente precisa ter entre 2 e 150 caracteres.");
        }

        //private void ValidarLocal()
        //{
          
        //    if (CheckEndereco)
        //        RuleFor(c => c.Endereco)
        //            .Null().When(c => c.CheckEndereco)
        //            .WithMessage("O cliente possui um endereço em seu nome");

        //    if (!CheckEndereco)
        //        RuleFor(c => c.Endereco)
        //            .NotNull().When(c => c.CheckEndereco == false)
        //            .WithMessage("O cliente não possui um endereço");

        //}

        #endregion

        public static class ClienteFactory
        {
            public static Cliente NovoClienteCompleto(
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
            Endereco endereco,
            Guid? masterUserId,
            Guid? generoId

            )

            {
                var cliente = new Cliente()
                {
                  Id = id,
                  NomeCompleto = nomecompleto,
                  CPF = cpf,
                  RG = rg,
                  TelResidencial = telresidencial,
                  TelCelular = telcelular,
                  Email = email,
                  RedeSocial = redesocial,
                  Data = data,
                  CheckEndereco = checkendereco,
                  Endereco = endereco,
                  GeneroId = generoId

                };

                if (masterUserId.HasValue)
                    cliente.MasterUserId = masterUserId.Value;

                if (!checkendereco)
                    cliente.Endereco = null;

                return cliente;
            }
        }

    }
}
