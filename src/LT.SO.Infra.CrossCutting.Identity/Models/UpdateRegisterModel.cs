using System.ComponentModel.DataAnnotations;

namespace LT.SO.Infra.CrossCutting.Identity.Models
{
    public class UpdateRegisterModel
    {
        [Required(ErrorMessage = "O nome é requerido")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O e-mail é requerido")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }        
     
        [Display(Name = "Altera Nome")]
        public string UpdateName { get; set; }
        
        [EmailAddress]
        [Display(Name = "Altera Email")]
        public string UpdateEmail { get; set; }        
    }
}
