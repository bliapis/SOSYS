using System.ComponentModel.DataAnnotations;

namespace LT.SO.Application.ViewModels.Gerencial.Usuario
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "O nome é requerido")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}