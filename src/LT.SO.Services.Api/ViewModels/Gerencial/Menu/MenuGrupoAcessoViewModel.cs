using System;
using System.ComponentModel.DataAnnotations;

namespace LT.SO.Services.Api.ViewModels.Gerencial.Menu
{
    public class MenuGrupoAcessoViewModel
    {
        [Key]
        public Guid MenuId { get; set; }

        [Key]
        public Guid GrupoAcessoId { get; set; }
    }
}