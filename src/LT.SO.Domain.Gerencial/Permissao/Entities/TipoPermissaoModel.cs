using System;
using System.Collections.Generic;
using FluentValidation;
using LT.SO.Domain.Core.Models;

namespace LT.SO.Domain.Permissoes.Permissao.Entities
{
    public class TipoPermissaoModel : Entity<TipoPermissaoModel>
    {
        public TipoPermissaoModel(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

        public TipoPermissaoModel() { }

        public string Nome { get; private set; }

        public virtual ICollection<PermissaoModel> Permissoes { get; private set; }

        public override bool IsValid()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        #region Validações
        private void Validar()
        {
            ValidarNome();
        }

        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do tipo deve ser informado")
                .Length(2, 150).WithMessage("O nome do tipo deve ter entre 2 e 150 caracteres");
        }
        #endregion

        public static class Factory
        {
            public static TipoPermissaoModel NovoTipo(Guid tipoId, string nome)
            {
                if (string.IsNullOrWhiteSpace(nome))
                    throw new ArgumentException("Não pode ser nulo", "nome");

                var tipoPermissao = new TipoPermissaoModel()
                {
                    Id = tipoId,
                    Nome = nome
                };

                return tipoPermissao;
            }
        }
    }
}