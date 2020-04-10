﻿using System.ComponentModel.DataAnnotations;

namespace LT.SO.Site.Models.Account
{
    public class ResetPasswordModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve estar entre {2} e {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação de senha não coincidem.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
    }
}