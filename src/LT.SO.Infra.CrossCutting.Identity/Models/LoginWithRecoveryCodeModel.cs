using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LT.SO.Infra.CrossCutting.Identity.Models
{
    public class LoginWithRecoveryCodeModel
    {
        [BindProperty]
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Codigo de recuperação")]
        public string RecoveryCode { get; set; }
    }
}