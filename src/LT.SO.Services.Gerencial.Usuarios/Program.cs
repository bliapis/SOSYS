using LT.SO.Domain.Gerencial.Usuario.Commands;
using LT.SO.Domain.Gerencial.Usuario.Events;
using LT.SO.Services.Common.Host;

namespace LT.SO.Services.Gerencial.Usuarios
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<CreateUsuarioCommand>()
                .SubscribeToEvent<UsuarioCreatedEvent>()
                .SubscribeToEvent<CreateUsuarioRejectedEvent>()
                .Build()
                .Run();
        }
    }
}