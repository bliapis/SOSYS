using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Domain.Core.Repository;
using LT.SO.Infra.CrossCutting.Bus;
using LT.SO.Infra.CrossCutting.AspNetFilters;
using LT.SO.Infra.CrossCutting.Identity.Models;
using LT.SO.Infra.CrossCutting.Identity.Services;
using LT.SO.Infra.CrossCutting.Log.Services;
using LT.SO.Infra.CrossCutting.Log.Interfaces;
using LT.SO.Infra.Data.Gerencial.Context;
using LT.SO.Infra.Data.Gerencial.UoW;
using LT.SO.Infra.Data.Log.Repository;
using LT.SO.Infra.Data.Gerencial.Repository;
using LT.SO.Infra.Data.Log.Seed;
using LT.SO.Infra.CrossCutting.Bus.RabbitMQ;
using LT.SO.Infra.Data.Common.Mongo;
using LT.SO.Infra.Data.Event;
using LT.SO.Domain.Permissoes.Permissao.Service;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Services;
using LT.SO.Domain.Permissoes.GrupoAcesso.Services;
using LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Service;
using LT.SO.Domain.Permissoes.Menu.Services;
using LT.SO.Domain.Permissoes.Menu.Interfaces.Service;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Repositories;
using LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Repository;
using LT.SO.Domain.Permissoes.Menu.Interfaces.Repository;
using LT.SO.Domain.Gerencial.Usuario.Services;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Service;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Repository;
using LT.SO.Domain.Gerencial.Usuario.Events;

namespace LT.SO.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Mapper.Configuration);

            //Identity
            // services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IUser, AspNetUser>();

            #region Domains

            services.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();

            #region Domain Gerencial

            services.AddScoped<IPermissaoService, PermissaoService>();
            services.AddScoped<ITipoPermissaoService, TipoPermissaoService>();
            services.AddScoped<IGrupoAcessoService, GrupoAcessoService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            // Domain - Commands
            //services.AddScoped<IHandler<CreateUsuarioCommand>, UsuarioCommandHandler>();

            // Domain - Events
            services.AddScoped<IHandler<UsuarioCreatedEvent>, UsuarioEventHandler>();

            #endregion

            #endregion

            #region Infra

            services.AddMongoDB(configuration);

            //Event
            services.AddScoped<IEventRepository<UsuarioCreatedEvent>, EventRepository<UsuarioCreatedEvent>>();
            services.AddScoped<IEventRepository<CreateUsuarioRejectedEvent>, EventRepository<CreateUsuarioRejectedEvent>>();

            //LogData
            services.AddScoped<IDatabaseSeeder<CustomMongoSeeder>, CustomMongoSeeder>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogAuditoriaRepository, LogAuditoriaRepository>();

            //Gerencial Data
            services.AddScoped<GerencialContext>();

            services.AddScoped<ITipoPermissaoRepository, TipoPermissaoRepository>();
            services.AddScoped<IPermissaoRepository, PermissaoRepository>();
            services.AddScoped<IGrupoAcessoRepository, GrupoAcessoRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region CrossCutting
            //Bus
            services.AddScoped<IBus, InMemoryBus>();
            services.AddRabbitMq(configuration);

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