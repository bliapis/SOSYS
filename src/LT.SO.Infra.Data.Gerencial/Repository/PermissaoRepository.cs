using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Permissoes.Permissao.Entities;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Repositories;
using LT.SO.Domain.Gerencial.Permissao.DTO;
using LT.SO.Infra.Data.Gerencial.Context;
using Dapper;

namespace LT.SO.Infra.Data.Gerencial.Repository
{
    public class PermissaoRepository : Repository<PermissaoModel>, IPermissaoRepository
    {
        public PermissaoRepository(GerencialContext context) : base(context)
        {
        }

        public override IEnumerable<PermissaoModel> GetAll()
        {
            var sql = "SELECT * FROM Permissao E ";

            return Db.Database.GetDbConnection().Query<PermissaoModel>(sql);
        }

        public override PermissaoModel GetById(Guid id)
        {
            var sql = @"SELECT * FROM Permissao E " +
                      "WHERE E.Id = @uid";

            var permissao = Db.Database.GetDbConnection().Query<PermissaoModel>(sql, new { uid = id });

            return permissao.SingleOrDefault();
        }

        public IEnumerable<PermissaoModel> GetByTipo(Guid tipoId)
        {
            var sql = "SELECT * FROM Permissao E "
                + "WHERE E.TipoId = @uid";

            return Db.Database.GetDbConnection().Query<PermissaoModel>(sql, new { uid = tipoId });
        }

        public PermissaoModel GetByTipoValor(Guid tipoId, string valor)
        {
            var sql = @"SELECT * FROM Permissao E " +
                      "WHERE E.TipoId = @uid and E.Valor = @unome";

            var permissao = Db.Database.GetDbConnection().Query<PermissaoModel>(sql, new { uid = tipoId, unome = valor });

            return permissao.SingleOrDefault();
        }

        public DataResult GetPaginado(PermissaoFilter filter)
        {
            DataResult result = new DataResult();
            string sql = "SP_GERENCIAL_SEL_PERMISSAO";

            var t = Db.Database.GetDbConnection()
                               .QueryMultiple(sql,
                                new
                                {
                                    Valor = filter.Valor,
                                    TipoId = filter.TipoId,
                                    Skip = filter.Skip,
                                    PageSize = filter.PageSize,
                                    SortColumn = filter.SortColumn,
                                    SorDirect = filter.SorDirect
                                }, commandType: CommandType.StoredProcedure);

            //result.LstRetorno = t.Read<PermissaoModel>(PermissaoModel).Cast<object>().ToList();

            var set2Func = new Func<PermissaoModel, TipoPermissaoModel, PermissaoModel>((p, c) => {
                p.SetTipoPermissao(c);
                return p;
            });
            
            result.LstRetorno = t.Read(set2Func, "Separador")
                                 .GroupBy(x => x.Id)
                                 .Select(x => {
                                     var permissao = x.First();
                                     //permissao.SetTipoPermissao(x.Select(p => TipoPermissaoModel.Factory.NovoTipo(p.TipoId, p.Tipo.Nome)).FirstOrDefault());
                                     permissao.SetTipoPermissao(x.Select(p => p.Tipo).FirstOrDefault());
                                     return permissao;
                                 })
                                 .Cast<object>().ToList();

            result.TotalRegistros = t.Read<int>().FirstOrDefault();

            return result;
        }
    }
}