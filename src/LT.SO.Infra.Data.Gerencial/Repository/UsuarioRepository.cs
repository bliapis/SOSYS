using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using LT.SO.Domain.Gerencial.Usuario.Entities;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Repository;
using LT.SO.Infra.Data.Gerencial.Context;
using Microsoft.EntityFrameworkCore;

namespace LT.SO.Infra.Data.Gerencial.Repository
{
    public class UsuarioRepository : Repository<UsuarioModel>, IUsuarioRepository
    {
        public UsuarioRepository(GerencialContext context) : base(context)
        {
        }

        public UsuarioModel GetByCpf(string cpf)
        {
            //return Find(u => u.CPF == cpf).FirstOrDefault();

            var sql = @"SELECT * FROM UsuarioApp E " +
                      "WHERE E.Cpf = @uid";

            var usuario = Db.Database.GetDbConnection().Query<UsuarioModel>(sql, new { uid = cpf });

            return usuario.SingleOrDefault();

        }

        public UsuarioModel GetByAspNetUserId(Guid aspNetUserId)
        {
            var sql = @"SELECT * FROM UsuarioApp E " +
                      "WHERE E.AspNetUserId = @uid";

            var usuario = Db.Database.GetDbConnection().Query<UsuarioModel>(sql, new { uid = aspNetUserId });

            return usuario.SingleOrDefault();
        }

        public UsuarioGrupoAcesso GetUsuarioGrupoAcesso(Guid usuarioId, Guid grupoAcessoId)
        {
            var sql = @"SELECT * FROM UsuarioGrupoAcesso E " +
                      "WHERE E.UsuarioId = @uid and E.GrupoAcessoId = @pid";
            
            var usuarioGrupoAcesso = Db.Database.GetDbConnection().Query<UsuarioGrupoAcesso>(sql, new { uid = usuarioId, pid = grupoAcessoId });
            
            return usuarioGrupoAcesso.SingleOrDefault();
        }

        public IEnumerable<UsuarioGrupoAcesso> GetUsuarioGrupoAcesso(Guid usuarioId)
        {
            var sql = @"SELECT * FROM UsuarioGrupoAcesso E " +
                      "WHERE E.UsuarioId = @uid";

            return Db.Database.GetDbConnection().Query<UsuarioGrupoAcesso>(sql, new { uid = usuarioId });
        }

        public void AddUsuarioGrupoAcesso(UsuarioGrupoAcesso usuarioGrupoAcesso)
        {
            Db.Add(usuarioGrupoAcesso);
        }

        public void RemoveUsuarioGrupoAcesso(Guid usuarioId, Guid grupoAcessoId)
        {
            var usuarioGrupoAcesso = GetUsuarioGrupoAcesso(usuarioId, grupoAcessoId);

            if (usuarioGrupoAcesso != null)
            {
                Db.Remove(usuarioGrupoAcesso);
            }
        }
    }
}