using System;
using System.Collections.Generic;
using LT.SO.Domain.Permissoes.Menu.Entities;

namespace LT.SO.Domain.Permissoes.Menu.Interfaces.Service
{
    public interface IMenuService : IDisposable
    {
        IEnumerable<MenuModel> ObterTodos();
        void Adicionar(MenuModel menu);
        MenuModel ObterPorId(Guid menuId);
        void Editar(MenuModel menu);
        void Remover(Guid menuId);

        IEnumerable<MenuModel> ObterMenusSemPai();
        IEnumerable<MenuModel> ObterMenusUsuario(Guid usuarioId);

        void AdicionarGrupoAcesso(MenusGruposAcesso menuGrupoAcesso);
        IEnumerable<MenusGruposAcesso> ObterGruposAcessoMenu(Guid menuId);
        void RemoverGrupoAcesso(Guid menuId, Guid grupoAcessoId);
    }
}