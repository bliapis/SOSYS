using System;
using System.Collections.Generic;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Gerencial.Permissao.DTO;
using LT.SO.Domain.Permissoes.Permissao.Entities;

namespace LT.SO.Domain.Permissoes.Permissao.Interfaces.Services
{
    public interface IPermissaoService : IDisposable
    {
        void Adicionar(PermissaoModel permissao);
        PermissaoModel ObterPorId(Guid permissaoId);
        IEnumerable<PermissaoModel> ObterPorTodos();
        IEnumerable<PermissaoModel> ObterPorTipo(Guid tipoId);
        void Editar(PermissaoModel permissao);
        void Remover(Guid permissaoId);

        DataResult ObterPaginado(PermissaoFilter filter);
    }
}