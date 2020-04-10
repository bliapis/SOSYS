using LT.SO.Domain.Core.DTO;

namespace LT.SO.Domain.Gerencial.Permissao.DTO
{
    public class TipoPermissaoFilter : BaseFilterDTO
    {
        string _nome;

        public string Nome { get { return _nome; } set { _nome = string.IsNullOrEmpty(value) ? string.Empty : value; } }
    }
}