using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LT.SO.Domain.Core;
using LT.SO.Domain.Core.Interfaces;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;
using RawRabbit.Pipe.Middleware;

namespace LT.SO.Infra.CrossCutting.Bus.RabbitMQ
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<Command>(this IBusClient bus,
            IHandlerMS<Command> handler) where Command : Message
            => bus.SubscribeAsync<Command>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseConsumeConfiguration(cfg =>
                cfg.FromQueue(GetQueueName<Command>())));

        public static Task WithEventHandlerAsync<Event>(this IBusClient bus,
            IHandlerMS<Event> handler) where Event : Message
            => bus.SubscribeAsync<Event>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseConsumeConfiguration(cfg =>
                cfg.FromQueue(GetQueueName<Event>())));

        //public static Task WithCommandHandlerAsync<Command>(this IBusClient bus,
        //    IHandlerMS<Command> handler) where Command : Message
        //    => bus.SubscribeAsync<Command>(msg => handler.HandleAsync(msg),
        //        ctx => ctx.UseConsumerConfiguration(cfg =>
        //        cfg.FromDeclaredQueue(RabbitMQ => RabbitMQ.WithName(GetQueueName<Command>()))));

        // public static Task WithEventHandlerAsync<Event>(this IBusClient bus,
        //     IHandlerMS<Event> handler) where Event : Message
        //     => bus.SubscribeAsync<Event>(msg => handler.HandleAsync(msg),
        //         ctx => ctx.UseConsumeConfiguration(cfg =>
        //         cfg.FromDeclaredQueue(RabbitMQ => RabbitMQ.WithName(GetQueueName<Event>()))));

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