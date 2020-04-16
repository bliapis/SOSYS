using System.ComponentModel.DataAnnotations;

namespace LT.SO.Application.ViewModels.Gerencial.Usuario
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}