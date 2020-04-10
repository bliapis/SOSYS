using System.ComponentModel.DataAnnotations;

namespace LT.SO.Infra.CrossCutting.Identity.Models
{
    public class EnableAuthenticatorModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Código de verificação")]
        public string Code { get; set; }
    }
}