using System.Threading.Tasks;
using LT.SO.Site.Models;
using LT.SO.Site.Models.Gerencial.TipoPermissao;
using Refit;

namespace LT.SO.Site.Services
{
    public interface ITipoPermissaoService
    {
        [Post("/api/v1/permissoes/tipo-permissao/pesquisar")]
        Task<ServiceResult> Pesquisar(TipoPermissaoFilter request);
    }
}