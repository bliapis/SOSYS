using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Gerencial.Interfaces;
using LT.SO.Domain.Gerencial.Permissao.DTO;
using LT.SO.Domain.Permissoes.Permissao.Entities;

namespace LT.SO.Domain.Permissoes.Permissao.Interfaces.Repositories
{
    public interface ITipoPermissaoRepository : IRepository<TipoPermissaoModel>
    {
        TipoPermissaoModel GetByNome(string nome);

        DataResult GetPaginado(TipoPermissaoFilter filter);
    }
}