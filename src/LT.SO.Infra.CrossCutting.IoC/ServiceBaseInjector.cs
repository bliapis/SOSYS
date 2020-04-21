using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Infra.Data.Common.Mongo;
using LT.SO.Infra.CrossCutting.Bus.RabbitMQ;
using Microsoft.AspNetCore.Http;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Infra.Data.Gerencial.UoW;
using LT.SO.Domain.Core.Bus;
using LT.SO.Infra.CrossCutting.Bus;
using LT.SO.Domain.Core.Repository;
using LT.SO.Infra.Data.Event;
using LT.SO.Infra.CrossCutting.Log.Services;
using Microsoft.Extensions.Logging;
using LT.SO.Infra.CrossCutting.AspNetFilters;

namespace LT.SO.Infra.CrossCutting.IoC
{
    public class ServiceBaseInjector
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddMongoDB(configuration);
            services.AddRabbitMq(configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBus, InMemoryBus>();
            services.AddScoped<IBusMS, BusMS>();

            services.AddScoped<IEventRepository<DomainNotification>, EventRepository<DomainNotification>>();

            #region Log
            //Log
            services.AddScoped<ILogService, LogService>();

            //Filtros
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
            services.AddScoped<GlobalActionLogger>();
            services.AddScoped<GlobalActionFilter>();
            #endregion
        }
    }
}