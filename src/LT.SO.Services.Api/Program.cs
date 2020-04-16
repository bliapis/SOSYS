using LT.SO.Domain.Gerencial.Usuario.Events;
using LT.SO.Services.Common.Host;

namespace LT.SO.Services.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToEvent<UsuarioCreatedEvent>()
                .Build()
                .Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();
    }
}