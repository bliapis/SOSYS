using System;
using System.ComponentModel.DataAnnotations;

namespace LT.SO.Application.ViewModels.Gerencial.Usuario
{
    public class UsuarioGrupoAcessoViewModel
    {
        [Key]
        public Guid UsuarioId { get; set; }

        [Key]
        public Guid GrupoAcessoId { get; set; }
    }
}