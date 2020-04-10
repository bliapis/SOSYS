using System;
using System.Collections.Generic;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Gerencial.GrupoAcesso.DTO;
using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;

namespace LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Service
{
    public interface IGrupoAcessoService : IDisposable
    {
        void Adicionar(GrupoAcessoModel grupoAcesso);
        GrupoAcessoModel ObterPorId(Guid grupoAcessoId);
        IEnumerable<GrupoAcessoModel> ObterTodos();
        void Editar(GrupoAcessoModel grupoAcesso);
        void Remover(Guid grupoAcessoId);

        void AdicionarPermissao(GrupoAcessoPermissao grupoAcessoPermissao);
        IEnumerable<GrupoAcessoPermissao> ObterGrupoAcessoPermissaoPorGrupoId(Guid grupoId);
        void RemoverGrupoAcessoPermissao(Guid grupoId, Guid permissaoId);

        DataResult ObterPaginado(GrupoAcessoFilter filter);
    }
}