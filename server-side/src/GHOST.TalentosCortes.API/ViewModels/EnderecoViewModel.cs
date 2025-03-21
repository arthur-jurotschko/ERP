﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GHOST.TalentosCortes.Services.API.ViewModels
{
    public class EnderecoViewModel
    {
        public EnderecoViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string CEP { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public Guid ClienteId { get; set; }

        public override string ToString()
        {
            return Logradouro + ", " + Numero + " - " + Bairro + ", " + Cidade + " - " + Estado;
        }
    }
}