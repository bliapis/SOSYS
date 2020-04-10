using System;
using System.Collections.Generic;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Gerencial.Permissao.DTO;
using LT.SO.Domain.Permissoes.Permissao.Entities;

namespace LT.SO.Domain.Permissoes.Permissao.Interfaces.Services
{
    public interface ITipoPermissaoService
    {
        void Adicionar(TipoPermissaoModel tipoPermissao);
        void Editar(TipoPermissaoModel tipoPermissao);
        void Remover(Guid tipoPermissaoId);

        TipoPermissaoModel ObterPorId(Guid tipoPermissaoId);
        IEnumerable<TipoPermissaoModel> ObterTodos();
        DataResult ObterPaginado(TipoPermissaoFilter filter);
    }
}