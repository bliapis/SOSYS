using System;
using System.Collections.Generic;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Gerencial.GrupoAcesso.DTO;
using LT.SO.Domain.Gerencial.Interfaces;
using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;

namespace LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Repository
{
    public interface IGrupoAcessoRepository : IRepository<GrupoAcessoModel>
    {
        GrupoAcessoModel GetByNome(string nome);

        GrupoAcessoPermissao GetGrupoAcessoPermissao(Guid grupoId, Guid permissaoId);
        void AddPermissao(GrupoAcessoPermissao grupoAcessoPermissao);
        IEnumerable<GrupoAcessoPermissao> GetGrupoAcessoPermissaoPorGrupoId(Guid grupoId);
        void RemovePermissaoGrupoAcesso(Guid grupoAcessoId, Guid permissaoId);

        DataResult GetPaginado(GrupoAcessoFilter filter);
    }
}