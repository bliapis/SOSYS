using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using LT.SO.Domain.Core;
using LT.SO.Domain.Core.Interfaces;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;
using Microsoft.Extensions.Configuration;

namespace LT.SO.Infra.CrossCutting.Bus.RabbitMQ
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<Command>(this IBusClient bus,
            IHandler<Command> handler) where Command : Message
            => bus.SubscribeAsync<Command>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseConsumerConfiguration(cfg =>
                cfg.FromDeclaredQueue(RabbitMQ => RabbitMQ.WithName(GetQueueName<Command>()))));

        public static Task WithEventHandlerAsync<Event>(this IBusClient bus,
            IHandler<Event> handler) where Event : Message
            => bus.SubscribeAsync<Event>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseConsumerConfiguration(cfg =>
                cfg.FromDeclaredQueue(RabbitMQ => RabbitMQ.WithName(GetQueueName<Event>()))));

        private static string GetQueueName<T>()
            => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration config)
        {
            var options = new RabbitMqOptions();
            var section = config.GetSection("rabbitmq");
            section.Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });
            services.AddSingleton<IBusClient>(_ => client);
        }
    }
}