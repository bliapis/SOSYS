using System;

namespace LT.SO.Domain.Gerencial.Usuario.Commands
{
    public class CreateUsuarioCommand : BaseUsuarioCommand
    {
        public string Usuario { get; private set; }
        public string Senha { get; private set; }

        public CreateUsuarioCommand(string usuario, string nome, string cpf, string email, string senha)
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;

            Usuario = usuario;
            Senha = senha;

            Nome = nome;
            CPF = cpf;
            Email = email;
        }
    }
}