using System;
using System.Threading.Tasks;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Repository;

namespace LT.SO.Domain.Gerencial.Usuario.Events
{
    public class UsuarioEventHandler :
        IHandler<UsuarioCreatedEvent>
    {
        private readonly IEventRepository<UsuarioCreatedEvent> _repo;

        public UsuarioEventHandler(
            IEventRepository<UsuarioCreatedEvent> repo)
        {
            _repo = repo;
        }

        public void Handle(UsuarioCreatedEvent message)
        {
            _repo.AddAsync(message);
            Console.WriteLine($"Lancamento criado: @{@message.Id}");
        }

        public async Task HandleAsync(UsuarioCreatedEvent message)
        {
            await _repo.AddAsync(message);
            Console.WriteLine($"Lancamento criado: @{@message.Id}");
        }
    }
}