using System.Collections.Generic;

namespace LT.SO.Domain.Core.Models
{
    public class DataResult
    {
        public int TotalRegistros { get; set; }
        public object Retorno { get; set; }
        public List<object> LstRetorno { get; set; }
    }
}