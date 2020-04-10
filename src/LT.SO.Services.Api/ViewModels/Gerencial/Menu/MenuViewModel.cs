using System;
using System.ComponentModel.DataAnnotations;

namespace LT.SO.Services.Api.ViewModels.Gerencial.Menu
{
    public class MenuViewModel
    {
        [Key]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "O Nome é requerido")]
        [MinLength(2, ErrorMessage = "O tamanho minimo do Nome é {1}")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo do Nome é {1}")]
        public string Nome { get; set; }

        public Guid? MenuPaiId { get; set; }
    }
}