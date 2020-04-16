using System;
using LT.SO.Domain.Core.Events;

namespace LT.SO.Domain.Gerencial.Usuario.Events
{
    public class CreateUsuarioRejectedEvent : BaseUsuarioEvent, IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }

        public CreateUsuarioRejectedEvent(string code, string reason, Guid id, string nome, string cpf, 
            string email, bool ativo, DateTime dataCadastro, Guid aspNetUserId)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
            Ativo = ativo;
            DataCadastro = dataCadastro;
            AspNetUserId = aspNetUserId;
            AggregateId = id;

            Code = code;
            Reason = reason;
        }
    }
}