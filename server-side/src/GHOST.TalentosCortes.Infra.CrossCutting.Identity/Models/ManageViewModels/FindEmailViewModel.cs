using System.ComponentModel.DataAnnotations;

namespace GHOST.TalentosCortes.Infra.CrossCutting.Identity.Models.ManageViewModels
{
    public class FindEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
