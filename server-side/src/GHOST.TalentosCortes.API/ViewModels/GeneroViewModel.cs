using System;
using System.ComponentModel.DataAnnotations;

namespace GHOST.TalentosCortes.Services.API.ViewModels
{
    public class GeneroViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
