using System.ComponentModel.DataAnnotations;

namespace LT.SO.Infra.CrossCutting.Identity.Models
{
    public class SetPasswordModel
    {
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