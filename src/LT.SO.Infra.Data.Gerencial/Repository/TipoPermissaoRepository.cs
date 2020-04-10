using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Dapper;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Permissoes.Permissao.Entities;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Repositories;
using LT.SO.Domain.Gerencial.Permissao.DTO;
using LT.SO.Infra.Data.Gerencial.Context;

namespace LT.SO.Infra.Data.Gerencial.Repository
{
    public class TipoPermissaoRepository : Repository<TipoPermissaoModel>, ITipoPermissaoRepository
    {
        public TipoPermissaoRepository(GerencialContext context) : base(context) { }

        public override IEnumerable<TipoPermissaoModel> GetAll()
        {
            var sql = "SELECT * FROM TipoPermissao E " +
                      "ORDER BY E.NOME ";

            return Db.Database.GetDbConnection().Query<TipoPermissaoModel>(sql);
        }

        public override TipoPermissaoModel GetById(Guid id)
        {
            var sql = @"SELECT * FROM TipoPermissao E " +
                      "WHERE E.Id = @uid";

            var tipoPermissao = Db.Database.GetDbConnection().Query<TipoPermissaoModel>(sql, new { uid = id });

            return tipoPermissao.SingleOrDefault();
        }

        public DataResult GetPaginado(TipoPermissaoFilter filter)
        {
            DataResult result = new DataResult();
            string sql = "SP_GERENCIAL_SEL_TipoPermissao";

            var t = Db.Database.GetDbConnection()
                               .QueryMultiple(sql, 
                                new {
                                        Nome = filter.Nome,
                                        Skip = filter.Skip,
                                        PageSize = filter.PageSize,
                                        SortColumn =  filter.SortColumn,
                                        SorDirect = filter.SorDirect
                                    }, commandType: CommandType.StoredProcedure);

            result.LstRetorno = t.Read<TipoPermissaoModel>().Cast<object>().ToList();
            result.TotalRegistros = t.Read<int>().FirstOrDefault();

            return result;
        }

        public TipoPermissaoModel GetByNome(string nome)
        {
            var sql = @"SELECT * FROM TipoPermissao E " +
                      "WHERE E.Nome = @uid";

            var tipoPermissao = Db.Database.GetDbConnection().Query<TipoPermissaoModel>(sql, new { uid = nome });

            return tipoPermissao.SingleOrDefault();
        }
    }
}