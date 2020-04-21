using System;
using System.Collections.Generic;
using FluentValidation;
using LT.SO.Domain.Core.Models;

namespace LT.SO.Domain.Gerencial.Usuario.Entities
{
    public class UsuarioModel : Entity<UsuarioModel>
    {
        public UsuarioModel(string nome, string cpf, string email, Guid aspNetUserId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            CPF = cpf;
            Email = email;
            //AspNetUserId = Guid.Parse(aspNetUserId);
            AspNetUserId = aspNetUserId;
            Ativo = true;
            DataCadastro = DateTime.Now;
        }

        public UsuarioModel() { } // constructor EF

        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Guid AspNetUserId { get; private set; }

        public virtual ICollection<UsuarioGrupoAcesso> UsuarioGruposAcesso { get; private set; }

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
                .NotEmpty().WithMessage("O nome deve ser informado")
                .Length(2, 50).WithMessage("O nome deve ter entre 2 e 50 caracteres");
        }
        #endregion

        public void DesativarUsuario()
        {
            Ativo = false;
        }

        public void AtivarUsuario()
        {
            Ativo = true;
        }

        public void SetAspNetUserId(string id)
        {
            AspNetUserId = Guid.Parse(id);
        }


        public static class UsuarioModelFactory
        {
            public static UsuarioModel NovoUsuarioCompleto(Guid id, string nome, string cpf, string email, Guid aspNetUserId, DateTime dataCadastro, bool ativo)
            {
                var usuario = new UsuarioModel()
                {
                    Id = id,
                    Nome = nome,
                    CPF = cpf,
                    Email = email,
                    AspNetUserId = aspNetUserId,
                    DataCadastro = dataCadastro,
                    Ativo = ativo
                };

                return usuario;
            }
        }
    }
}