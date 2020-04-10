using System.ComponentModel.DataAnnotations;

namespace LT.SO.Site.Models.Account
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "O nome é requerido")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}