using System.Collections.Generic;
using Newtonsoft.Json;

namespace LT.SO.Site.Models
{
    public class ServiceResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("errors")]
        public List<string> Erros { get; set; }
    }


    public class PaginatedResult
    {
        [JsonProperty("totalRegistros")]
        public int TotalRegistros { get; set; }

        [JsonProperty("retorno")]
        public object Retorno { get; set; }

        [JsonProperty("lstRetorno")]
        public object LstRetorno { get; set; }
    }
}