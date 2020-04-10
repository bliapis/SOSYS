using System.ComponentModel.DataAnnotations;

namespace LT.SO.Infra.CrossCutting.Identity.Models
{
    public class DeleteModel
    {
        [Required(ErrorMessage = "O nome é requerido")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O e-mail é requerido")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
