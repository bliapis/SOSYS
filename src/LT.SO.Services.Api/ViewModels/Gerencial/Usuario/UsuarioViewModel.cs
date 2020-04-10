using System;
using System.ComponentModel.DataAnnotations;

namespace LT.SO.Services.Api.ViewModels.Gerencial.Usuario
{
    public class UsuarioViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Preencha um nome de usuário.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "O Nome é requerido")]
        [MinLength(2, ErrorMessage = "O tamanho minimo do Nome é {1}")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo do Nome é {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é requerido")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "CPF incorreto.")]
        public string CPF { get; private set; }

        [Required(ErrorMessage = "O Email é requerido")]
        [EmailAddress(ErrorMessage = "Preencha um e-mail valido.")]
        public string Email { get; set; }
    }
}