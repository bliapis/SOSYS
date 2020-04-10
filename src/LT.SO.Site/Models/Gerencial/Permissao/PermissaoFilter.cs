using System;
using System.ComponentModel.DataAnnotations;

namespace LT.SO.Site.Models.Gerencial.Permissao
{
    public class PermissaoFilter : DataTableAjaxPostModel
    {
        public string Valor { get; set; }

        [Display(Name = "Tipo Permissão")]
        public Guid? TipoId { get; set; }
    }
}