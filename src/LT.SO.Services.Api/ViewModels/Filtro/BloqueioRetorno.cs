using System.Collections.Generic;

namespace LT.SO.Services.Api.ViewModels.Filtro
{
    public class BloqueioRetorno
    {
        public int TotalRegistros { get; set; }
        public BloqueioViewModel Retorno { get; set; }
        public List<BloqueioViewModel> LstRetorno { get; set; }
    }
}