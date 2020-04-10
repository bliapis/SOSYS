using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;
using System;

namespace LT.SO.Domain.Permissoes.Menu.Entities
{
    public class MenusGruposAcesso
    {
        public Guid MenuId { get; set; }
        public MenuModel Menu { get; set; }

        public Guid GrupoAcessoId { get; set; }
        public GrupoAcessoModel GrupoAcesso { get; set; }
    }
}