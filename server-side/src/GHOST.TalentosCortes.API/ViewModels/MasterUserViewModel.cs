﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GHOST.TalentosCortes.Services.API.ViewModels
{
    public class MasterUserViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é requerido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é requerido")]
        [StringLength(11)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O e-mail é requerido")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        public string Email { get; set; }
    }
}
