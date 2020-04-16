using System;
using System.ComponentModel.DataAnnotations;

namespace LT.SO.Application.ViewModels.Gerencial.Usuario
{
    public class AddUsuarioViewModel
    {
        [Required(ErrorMessage = "Preencha um nome de usuário.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Preencha uma senha.")]
        [StringLength(100, ErrorMessage = "O {0} deve estar entre {2} e {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha", ErrorMessage = "A senha e a confirmação de senha não coincidem.")]
        public string ConfirmaSenha { get; set; }

        [Required(ErrorMessage = "O Nome é requerido")]
        [MinLength(2, ErrorMessage = "O tamanho minimo do Nome é {1}")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo do Nome é {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é requerido")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "CPF incorreto.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O Email é requerido")]
        [EmailAddress(ErrorMessage = "Preencha um e-mail valido.")]
        public string Email { get; set; }
    }
}