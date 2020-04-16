using LT.SO.Domain.Gerencial.Usuario.Commands;
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
                .Build()
                .Run();
        }
    }
}