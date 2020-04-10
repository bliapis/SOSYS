using System;
using System.Collections.Generic;
using LT.SO.Domain.Gerencial.Interfaces;
using LT.SO.Domain.Permissoes.Menu.Entities;

namespace LT.SO.Domain.Permissoes.Menu.Interfaces.Repository
{
    public interface IMenuRepository : IRepository<MenuModel>
    {
        MenuModel GetByNome(string nome);
        IEnumerable<MenuModel> GetByNomeEPai(string nome, Guid? menuPaiId);

        MenusGruposAcesso GetMenuGrupoAcesso(Guid menuId, Guid grupoAcessoId);
        IEnumerable<MenusGruposAcesso> GetMenuGrupoAcessoPorMenuId(Guid menuId);
        IEnumerable<MenusGruposAcesso> GetMenuGrupoAcessoPorGrupoId(Guid grupoId);
        void AddGrupoAcesso(MenusGruposAcesso menuGrupoAcesso);
        void RemoveMenuGrupoAcesso(Guid menuId, Guid grupoAcessoId);
    }
}