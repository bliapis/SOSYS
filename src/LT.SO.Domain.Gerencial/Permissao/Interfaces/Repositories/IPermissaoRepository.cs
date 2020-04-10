using System;
using System.Collections.Generic;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Gerencial.Interfaces;
using LT.SO.Domain.Gerencial.Permissao.DTO;
using LT.SO.Domain.Permissoes.Permissao.Entities;

namespace LT.SO.Domain.Permissoes.Permissao.Interfaces.Repositories
{
    public interface IPermissaoRepository : IRepository<PermissaoModel>
    {
        IEnumerable<PermissaoModel> GetByTipo(Guid tipoId);
        PermissaoModel GetByTipoValor(Guid tipoId, string valor);

        DataResult GetPaginado(PermissaoFilter filter);
    }
}