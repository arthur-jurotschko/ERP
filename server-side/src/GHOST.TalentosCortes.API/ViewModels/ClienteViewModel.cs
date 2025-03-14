using System;
using System.ComponentModel.DataAnnotations;

namespace GHOST.TalentosCortes.Services.API.ViewModels
{
    public class ClienteViewModel
    {
        public ClienteViewModel()
        {
            Id = Guid.NewGuid();
            Endereco = new EnderecoViewModel();
            Genero = new GeneroViewModel();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome Completo é requerido")]
        [MinLength(2, ErrorMessage = "O tamanho minimo do Nome é {1}")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo do Nome é {1}")]
        [Display(Name = "Nome Completo do Cliente")]
        public string NomeCompleto { get;  set; }


        public string CPF { get;  set; }
        public string RG { get;  set; }
        public string TelResidencial { get;  set; }
        public string TelCelular { get;  set; }
        public string Email { get;  set; }
        public string RedeSocial { get;  set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "A data é requerida")]
        public DateTime Data { get;  set; }

        public bool CheckEndereco { get;  set; }

        public EnderecoViewModel Endereco { get;  set; }
        public GeneroViewModel Genero { get;  set; }
        public Guid GeneroId { get; set; }
        public Guid MasterUserId { get; set; }
    }
}
