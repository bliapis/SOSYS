using System.Collections.Generic;

namespace LT.SO.Services.Api.Models
{
    public class AuditConfig
    {
        public bool Active { get; set; }

        public List<string> Controllers { get; set; }
    }
}