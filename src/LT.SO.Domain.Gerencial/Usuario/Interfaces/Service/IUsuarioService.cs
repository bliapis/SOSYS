using System;
using System.Collections.Generic;
using LT.SO.Domain.Gerencial.Usuario.Entities;

namespace LT.SO.Domain.Gerencial.Usuario.Interfaces.Service
{
    public interface IUsuarioService : IDisposable
    {
        void Adicionar(UsuarioModel usuario);
        UsuarioModel ObterPorId(Guid usuarioId);
        UsuarioModel ObterPorAspNetUserId(string aspNetUserId);
        IEnumerable<UsuarioModel> ObterTodos();
        void Editar(UsuarioModel usuario);
        void Remover(Guid usuarioId);

        void AdicionarGrupoAcesso(UsuarioGrupoAcesso usuarioGrupoAcesso);
        IEnumerable<UsuarioGrupoAcesso> ObterUsuarioGrupoAcessoPorUsuarioId(Guid usuarioId);
        void RemoverUsuarioGrupoAcesso(Guid usuarioId, Guid grupoAcessoId);
    }
}