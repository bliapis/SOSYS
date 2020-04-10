using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Dapper;
using LT.SO.Infra.Data.Gerencial.Context;
using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;
using LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Repository;
using System.Linq;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Gerencial.GrupoAcesso.DTO;
using System.Data;

namespace LT.SO.Infra.Data.Gerencial.Repository
{
    public class GrupoAcessoRepository : Repository<GrupoAcessoModel>, IGrupoAcessoRepository
    {
        public GrupoAcessoRepository(GerencialContext context) : base(context)
        {

        }

        public override IEnumerable<GrupoAcessoModel> GetAll()
        {
            var sql = "SELECT * FROM GrupoAcesso E ";

            return Db.Database.GetDbConnection().Query<GrupoAcessoModel>(sql);
        }

        public override GrupoAcessoModel GetById(Guid id)
        {
            var sql = @"SELECT * FROM GrupoAcesso E " +
                      "WHERE E.Id = @uid";

            var permissao = Db.Database.GetDbConnection().Query<GrupoAcessoModel>(sql, new { uid = id });

            return permissao.SingleOrDefault();
        }

        public GrupoAcessoModel GetByNome(string nome)
        {
            var sql = @"SELECT * FROM GrupoAcesso E " +
                      "WHERE E.Nome = @uid";

            var tipoPermissao = Db.Database.GetDbConnection().Query<GrupoAcessoModel>(sql, new { uid = nome });

            return tipoPermissao.SingleOrDefault();
        }

        //public IEnumerable<GrupoAcessoUsuario> GetGrupoAcessoUsuariosByUserId(Guid userId)
        //{
        //    var sql = @"SELECT * FROM GrupoAcessoUsuario E " +
        //              "WHERE E.UserId = @uid";
        //
        //    return Db.Database.GetDbConnection().Query<GrupoAcessoUsuario>(sql, new { uid = userId });
        //}

        public GrupoAcessoPermissao GetGrupoAcessoPermissao(Guid grupoId, Guid permissaoId)
        {
            var sql = @"SELECT * FROM GrupoAcessoPermissao E " +
                      "WHERE E.GrupoAcessoId = @uid and E.PermissaoId = @pid";

            var grupoAcessoPermissao = Db.Database.GetDbConnection().Query<GrupoAcessoPermissao>(sql, new { uid = grupoId, pid = permissaoId });

            return grupoAcessoPermissao.SingleOrDefault();
        }

        public DataResult GetPaginado(GrupoAcessoFilter filter)
        {
            DataResult result = new DataResult();
            string sql = "SP_GERENCIAL_SEL_GRUPOACESSO";

            var t = Db.Database.GetDbConnection()
                               .QueryMultiple(sql,
                                new
                                {
                                    Nome = filter.Nome,
                                    TipoId = filter.TipoId,
                                    Skip = filter.Skip,
                                    PageSize = filter.PageSize,
                                    SortColumn = filter.SortColumn,
                                    SorDirect = filter.SorDirect
                                }, commandType: CommandType.StoredProcedure);

            result.TotalRegistros = t.Read<int>().FirstOrDefault();

            if (result.TotalRegistros > 0)
                result.LstRetorno = t.Read<GrupoAcessoModel>().Cast<object>().ToList();

            return result;
        }

        public void AddPermissao(GrupoAcessoPermissao grupoAcessoPermissao)
        {
            Db.Add(grupoAcessoPermissao);
        }

        public IEnumerable<GrupoAcessoPermissao> GetGrupoAcessoPermissaoPorGrupoId(Guid grupoId)
        {
            var sql = @"SELECT * FROM GrupoAcessoPermissao E " +
                      "WHERE E.GrupoAcessoId = @uid";

            return Db.Database.GetDbConnection().Query<GrupoAcessoPermissao>(sql, new { uid = grupoId });
        }

        public void RemovePermissaoGrupoAcesso(Guid grupoAcessoId, Guid permissaoId)
        {
            var grupoAcessoPermissao = GetGrupoAcessoPermissao(grupoAcessoId, permissaoId);

            if (grupoAcessoPermissao != null)
            {
                Db.Remove(grupoAcessoPermissao);
            }
        }
    }
}