using System;
using System.ComponentModel.DataAnnotations;

namespace LT.SO.Site.Models.Gerencial.GrupoAcesso
{
    public class GrupoAcessoFilter : DataTableAjaxPostModel
    {
        public string Nome { get; set; }

        [Display(Name = "Tipo Permissão")]
        public Guid? TipoId { get; set; }
    }
}