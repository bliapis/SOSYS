using System;
using System.Collections.Generic;
using FluentValidation;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Gerencial.Usuario.Entities;
using LT.SO.Domain.Permissoes.Menu.Entities;

namespace LT.SO.Domain.Permissoes.GrupoAcesso.Entities
{
    public class GrupoAcessoModel : Entity<GrupoAcessoModel>
    {
        public GrupoAcessoModel(string nome)
        {
            Id = Guid.NewGuid();

            Nome = nome;
        }

        public GrupoAcessoModel() { }


        public string Nome { get; private set; }

        public virtual ICollection<GrupoAcessoPermissao> GruposAcessoPermissaos { get; set; }
        public virtual ICollection<UsuarioGrupoAcesso> GruposAcessoUsuario { get; set; }
        public virtual ICollection<MenusGruposAcesso> MenusGruposAcesso { get; set; }


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
                .NotEmpty().WithMessage("O valor da permissão deve ser informado")
                .Length(2, 50).WithMessage("O valor da permissão deve ter entre 2 e 50 caracteres");
        }
        #endregion
    }
}