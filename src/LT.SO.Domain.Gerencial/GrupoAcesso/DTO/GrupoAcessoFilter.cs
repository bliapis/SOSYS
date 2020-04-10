using System;
using LT.SO.Domain.Core.DTO;

namespace LT.SO.Domain.Gerencial.GrupoAcesso.DTO
{
    public class GrupoAcessoFilter : BaseFilterDTO
    {
        string _nome;

        public string Nome { get { return _nome; } set { _nome = string.IsNullOrEmpty(value) ? string.Empty : value; } }

        public Guid? TipoId { get; set; }
    }
}