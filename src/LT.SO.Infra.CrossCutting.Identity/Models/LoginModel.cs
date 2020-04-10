using System.ComponentModel.DataAnnotations;

namespace LT.SO.Infra.CrossCutting.Identity.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}