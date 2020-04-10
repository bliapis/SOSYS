using LT.SO.Domain.Permissoes.Permissao.Entities;
using System;

namespace LT.SO.Domain.Permissoes.GrupoAcesso.Entities
{
    public class GrupoAcessoPermissao
    {
        public Guid GrupoAcessoId { get; set; }
        public virtual GrupoAcessoModel GrupoAcesso { get; set; }

        public Guid PermissaoId { get; set; }
        public virtual PermissaoModel Permissao { get; set; }
    }
}