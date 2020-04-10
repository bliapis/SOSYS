using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;
using System;
namespace LT.SO.Domain.Gerencial.Usuario.Entities
{
    public class UsuarioGrupoAcesso
    {
        public Guid UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }

        public Guid GrupoAcessoId { get; set; }
        public GrupoAcessoModel GrupoAcesso { get; set; }
    }
}