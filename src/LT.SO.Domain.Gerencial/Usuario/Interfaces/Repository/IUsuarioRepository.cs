using System;
using System.Collections.Generic;
using LT.SO.Domain.Gerencial.Interfaces;
using LT.SO.Domain.Gerencial.Usuario.Entities;

namespace LT.SO.Domain.Gerencial.Usuario.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<UsuarioModel>
    {
        UsuarioModel GetByCpf(string cpf);
        UsuarioModel GetByAspNetUserId(Guid aspNetUserId);

        UsuarioGrupoAcesso GetUsuarioGrupoAcesso(Guid usuarioId, Guid grupoAcessoId);
        IEnumerable<UsuarioGrupoAcesso> GetUsuarioGrupoAcesso(Guid usuarioId);
        void AddUsuarioGrupoAcesso(UsuarioGrupoAcesso usuarioGrupoAcesso);
        void RemoveUsuarioGrupoAcesso(Guid usuarioId, Guid grupoAcessoId);
    }
}