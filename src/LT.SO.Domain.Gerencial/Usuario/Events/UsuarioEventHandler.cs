using System;
using System.Threading.Tasks;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Repository;

namespace LT.SO.Domain.Gerencial.Usuario.Events
{
    public class UsuarioEventHandler :
        IHandlerMS<UsuarioCreatedEvent>,
        IHandlerMS<CreateUsuarioRejectedEvent>
    {
        //TODO: Melhorar essa implementação
        private readonly IEventRepository<UsuarioCreatedEvent> _repoUsuarioCreated;
        private readonly IEventRepository<CreateUsuarioRejectedEvent> _repoUsuarioRejected;

        public UsuarioEventHandler(
            IEventRepository<UsuarioCreatedEvent> repoUsuarioCreated,
            IEventRepository<CreateUsuarioRejectedEvent> repoUsuarioRejected)
        {
            _repoUsuarioCreated = repoUsuarioCreated;
            _repoUsuarioRejected = repoUsuarioRejected;
        }

        public async Task HandleAsync(UsuarioCreatedEvent message)
        {

            await _repoUsuarioCreated.AddAsync(message);
            Console.WriteLine($"Usuario criado: @{@message.Id}");
        }

        public async Task HandleAsync(CreateUsuarioRejectedEvent message)
        {
            await _repoUsuarioRejected.AddAsync(message);
            Console.WriteLine($"Usuario rejeitado pelo serviço: @{@message.Id}");
        }
    }
}