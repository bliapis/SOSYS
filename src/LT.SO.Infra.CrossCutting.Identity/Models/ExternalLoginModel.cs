using System.ComponentModel.DataAnnotations;

namespace LT.SO.Infra.CrossCutting.Identity.Models
{
    public class ExternalLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}