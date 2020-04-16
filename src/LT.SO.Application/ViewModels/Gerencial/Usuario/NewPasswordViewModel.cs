using System.ComponentModel.DataAnnotations;

namespace LT.SO.Application.ViewModels.Gerencial.Usuario
{
    public class NewPasswordViewModel
    {
        [Required(ErrorMessage = "O nome é requerido")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O {0} Deve estar entre {2} e {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova senha")]
        [Compare("NewPassword", ErrorMessage = "A nova senha e a confirmação de senha não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}