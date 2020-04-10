using System.ComponentModel.DataAnnotations;

namespace LT.SO.Infra.CrossCutting.Identity.Models
{
    public class LoginWith2faModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "O {0} deve estar entre {2} e {1} carácteres.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Código de autenticação")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Lembrar desse computador")]
        public bool RememberMachine { get; set; }
    }
}