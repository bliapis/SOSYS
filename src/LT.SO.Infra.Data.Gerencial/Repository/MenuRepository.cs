using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Dapper;
using LT.SO.Domain.Permissoes.Menu.Entities;
using LT.SO.Domain.Permissoes.Menu.Interfaces.Repository;
using LT.SO.Infra.Data.Gerencial.Context;

namespace LT.SO.Infra.Data.Gerencial.Repository
{
    public class MenuRepository : Repository<MenuModel>, IMenuRepository
    {
        public MenuRepository(GerencialContext context) : base(context)
        {
        }

        public MenuModel GetByNome(string nome)
        {
            var sql = @"SELECT * FROM MenuApp E " +
                      "WHERE E.Nome = @uid";

            var menu = Db.Database.GetDbConnection().Query<MenuModel>(sql, new { uid = nome });

            return menu.SingleOrDefault();
        }

        public IEnumerable<MenuModel> GetByNomeEPai(string nome, Guid? menuPaiId)
        {
            var sql = @"SELECT * FROM MenuApp E " +
                      "WHERE E.Nome = @uid and E.MenuPaiId = @pid";

            return Db.Database.GetDbConnection().Query<MenuModel>(sql, new { uid = nome, pid = menuPaiId });
        }

        public override IEnumerable<MenuModel> GetAll()
        {
            var sql = "SELECT * FROM MenuApp E ";

            return Db.Database.GetDbConnection().Query<MenuModel>(sql);
        }

        public override MenuModel GetById(Guid id)
        {
            var sql = @"SELECT * FROM MenuApp E " +
                      "WHERE E.Id = @uid";

            var permissao = Db.Database.GetDbConnection().Query<MenuModel>(sql, new { uid = id });

            return permissao.SingleOrDefault();
        }

        #region MenuGrupoAcesso
        public void AddGrupoAcesso(MenusGruposAcesso menuGrupoAcesso)
        {
            Db.Add(menuGrupoAcesso);
        }

        public MenusGruposAcesso GetMenuGrupoAcesso(Guid menuId, Guid grupoAcessoId)
        {
            var sql = @"SELECT * FROM MenuAppGruposAcesso E " +
                      "WHERE E.MenuId = @uid and E.GrupoAcessoId = @pid";

            var grupoAcessoPermissao = Db.Database.GetDbConnection().Query<MenusGruposAcesso>(sql, new { uid = menuId, pid = grupoAcessoId });

            return grupoAcessoPermissao.SingleOrDefault();
        }

        public IEnumerable<MenusGruposAcesso> GetMenuGrupoAcessoPorMenuId(Guid menuId)
        {
            var sql = @"SELECT * FROM MenuAppGruposAcesso E " +
                      "WHERE E.MenuId = @uid";

            return Db.Database.GetDbConnection().Query<MenusGruposAcesso>(sql, new { uid = menuId });
        }

        public IEnumerable<MenusGruposAcesso> GetMenuGrupoAcessoPorGrupoId(Guid grupoId)
        {
            var sql = @"SELECT * FROM MenuAppGruposAcesso E " +
                      "WHERE E.GrupoAcessoId = @uid";

            return Db.Database.GetDbConnection().Query<MenusGruposAcesso>(sql, new { uid = grupoId });
        }

        public void RemoveMenuGrupoAcesso(Guid menuId, Guid grupoAcessoId)
        {
            var menuGrupoAcesso = GetMenuGrupoAcesso(menuId, grupoAcessoId);

            if (menuGrupoAcesso != null)
            {
                Db.Remove(menuGrupoAcesso);
            }
        }
        #endregion
    }
}