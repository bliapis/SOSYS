using System;
using System.ComponentModel.DataAnnotations;

namespace LT.SO.Services.Api.ViewModels.Gerencial.GrupoAcesso
{
    public class GrupoAcessoPermissaoViewModel
    {
        [Key]
        public Guid GrupoAcessoId { get; set; }

        [Key]
        public Guid PermissaoId { get; set; }
    }
}