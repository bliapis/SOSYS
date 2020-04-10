using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace LT.SO.Site.Models.Account
{
    public class LoginModel
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "É necessário preencher o usuário")]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "É necessário preencher a senha")]
        [DataType(DataType.Password)]
        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class DataLogin
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public DateTimeOffset Validade { get; set; }

        [JsonProperty("user")]
        public User Usuario { get; set; }
    }

    public class User
    {
        [JsonProperty("usuario")]
        public string Usuario { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstPass")]
        public bool FirstPass { get; set; }
    }



}