using System;
using LT.SO.Domain.Core.DTO;

namespace LT.SO.Domain.Gerencial.Permissao.DTO
{
    public class PermissaoFilter : BaseFilterDTO
    {
        string _valor;

        public string Valor { get { return _valor; } set { _valor = string.IsNullOrEmpty(value) ? string.Empty : value; } }

        public Guid? TipoId { get; set; }
    }
}