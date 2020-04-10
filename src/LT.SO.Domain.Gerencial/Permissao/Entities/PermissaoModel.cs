using System;
using System.Collections.Generic;
using FluentValidation;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;

namespace LT.SO.Domain.Permissoes.Permissao.Entities
{
    public class PermissaoModel : Entity<PermissaoModel>
    {
        public PermissaoModel(string valor, Guid tipoId)
        {
            Id = Guid.NewGuid();
            Valor = valor;
            TipoId = tipoId;
        }

        public PermissaoModel() { }


        public string Valor { get; private set; }

        public Guid TipoId { get; private set; }

        public virtual TipoPermissaoModel Tipo { get; private set; }

        public ICollection<GrupoAcessoPermissao> GruposAcessoPermissaos { get; set; }


        public override bool IsValid()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        #region Validações
        private void Validar()
        {
            ValidarValor();
        }

        private void ValidarValor()
        {
            RuleFor(c => c.Valor)
                .NotEmpty().WithMessage("O valor da permissão deve ser informado")
                .Length(2, 30).WithMessage("O valor da permissão deve ter entre 2 e 30 caracteres");
        }
        #endregion

        public void SetTipoPermissao(TipoPermissaoModel tipo)
        {
            this.Tipo = tipo;
        }
    }
}