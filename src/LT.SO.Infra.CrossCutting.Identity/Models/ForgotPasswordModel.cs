using System.ComponentModel.DataAnnotations;

namespace LT.SO.Infra.CrossCutting.Identity.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "O nome é requerido")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}
