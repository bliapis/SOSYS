using System;

namespace LT.SO.Services.Api.ViewModels.Filtro
{
    public class BloqueioViewModel
    {
        public int Linha { get; set; }
        public int IdBloqueio { get; set; }
        public int IdFiltro { get; set; }
        public string Descricao { get; set; }
        public long Raiz { get; set; }
        public string TipoCliente { get; set; }
        public string Nome { get; set; }
        public string BloqueadoPor { get; set; }
        public DateTime DataBloqueio { get; set; }
        public string Plataforma { get; set; }
        public int IdPlataforma { get; set; }
        public string ClasseServico { get; set; }
        public string MotivoBloqueio { get; set; }
        public string ModoCriacaoRegistro { get; set; }
        public string Chave { get; set; }
        public int Nrc { get; set; }
        public string Loja { get; set; }
        public string Filial { get; set; }
        public string Codigo { get; set; }
    }
}