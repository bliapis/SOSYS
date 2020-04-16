using System;

namespace LT.SO.Domain.Gerencial.Usuario.Commands
{
    public class CreateUsuarioCommand : BaseUsuarioCommand
    {
        public CreateUsuarioCommand(string nome, string cpf, string email, bool ativo, DateTime dataCadastro, Guid aspNetUserId)
        {
            Nome = nome;
            CPF = cpf;
            Email = email;
            Ativo = ativo;
            DataCadastro = dataCadastro;
            AspNetUserId = aspNetUserId;
        }
    }
}