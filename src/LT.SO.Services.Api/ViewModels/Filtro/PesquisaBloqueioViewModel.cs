using System.ComponentModel.DataAnnotations;

namespace LT.SO.Services.Api.ViewModels.Filtro
{
    public class PesquisaBloqueioViewModel
    {
        [Display(Name = "Plataforma")]
        public int IdPlataforma { get; set; }

        [Display(Name = "Bloqueio")]
        public int ModoCriacaoReg { get; set; }

        [Display(Name = "Filtro")]
        [Required(ErrorMessage = "O filtro é requerido.")]
        public int IdFiltro { get; set; }

        [Display(Name = "Raiz")]
        [Required(ErrorMessage = "A raiz é requerida.")]
        public long Raiz { get; set; }

        [Display(Name = "Classe de Serviço")]
        public string ClasseServico { get; set; }

        [Display(Name = "Linha De")]
        public int LinhaDe { get; set; }

        [Display(Name = "Linha Ate")]
        public int LinhaAte { get; set; }
    }
}