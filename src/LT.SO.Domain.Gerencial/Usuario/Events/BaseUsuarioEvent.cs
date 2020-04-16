using System;
using LT.SO.Domain.Core.Events;

namespace LT.SO.Domain.Gerencial.Usuario.Events
{
    public class BaseUsuarioEvent : Event
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public string CPF { get; protected set; }
        public string Email { get; protected set; }
        public bool Ativo { get; protected set; }
        public DateTime DataCadastro { get; protected set; }

        public Guid AspNetUserId { get; protected set; }
    }
}