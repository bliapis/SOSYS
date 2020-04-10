using Newtonsoft.Json;

namespace LT.SO.Site.Models.Account
{
    public class UsuarioModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("usuario")]
        public string Usuario { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}