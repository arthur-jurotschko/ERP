﻿using System.ComponentModel.DataAnnotations;

namespace GHOST.TalentosCortes.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
