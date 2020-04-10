using System;
using System.ComponentModel.DataAnnotations;

namespace LT.SO.Services.Api.ViewModels.Gerencial.Permissoes
{
    public class PermissaoViewModel
    {
        [Key]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "O Valor é requerido")]
        [MinLength(2, ErrorMessage = "O tamanho minimo do Valor é {1}")]
        [MaxLength(30, ErrorMessage = "O tamanho máximo do Valor é {1}")]
        public string Valor { get; set; }

        [Required(ErrorMessage = "É necessário informar um Tipo de Permissão")]
        public Guid TipoId { get; set; }

        public string TipoNome { get; set; }
    }
}
