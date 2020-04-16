using System;
using System.Collections.Generic;
using System.Text;

namespace LT.SO.Domain.Gerencial.Usuario.Events
{
    public class UsuarioCreatedEvent : BaseUsuarioEvent
    {
        public UsuarioCreatedEvent(Guid id, string nome, string cpf, string email, bool ativo, DateTime dataCadastro, Guid aspNetUserId)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
            Ativo = ativo;
            DataCadastro = dataCadastro;
            AspNetUserId = aspNetUserId;

            AggregateId = id;
        }
    }
}