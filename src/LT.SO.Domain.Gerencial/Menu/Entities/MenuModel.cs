using System;
using System.Collections.Generic;
using FluentValidation;
using LT.SO.Domain.Core.Models;

namespace LT.SO.Domain.Permissoes.Menu.Entities
{
    public class MenuModel : Entity<MenuModel>
    {
        public MenuModel(string nome)
        {
            Nome = nome;
        }

        public MenuModel() { }


        public string Nome { get; private set; }
        public Guid? MenuPaiId { get; private set; }

        public virtual MenuModel MenuPai { get; private set; }
        public virtual ICollection<MenuModel> MenusFilhos { get; private set; }
        public virtual ICollection<MenusGruposAcesso> MenusGruposAcesso { get; private set; }

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
                .NotEmpty().WithMessage("O nome do menu deve ser informado")
                .Length(2, 50).WithMessage("O nome do menu deve ter entre 2 e 50 caracteres");
        }
        #endregion
    }
}