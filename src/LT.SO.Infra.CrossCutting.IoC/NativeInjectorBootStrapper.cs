using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Infra.CrossCutting.Bus;
using LT.SO.Infra.CrossCutting.AspNetFilters;
using LT.SO.Infra.CrossCutting.Identity.Models;
using LT.SO.Infra.CrossCutting.Identity.Services;
using LT.SO.Infra.CrossCutting.Log.Services;
using LT.SO.Infra.CrossCutting.Log.Interfaces;
using LT.SO.Infra.Data.Log.Context;
using LT.SO.Infra.Data.Log.Repository;
using LT.SO.Domain.Permissoes.Permissao.Service;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Services;
using LT.SO.Domain.Permissoes.GrupoAcesso.Services;
using LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Service;
using LT.SO.Domain.Permissoes.Menu.Services;
using LT.SO.Domain.Permissoes.Menu.Interfaces.Service;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Repositories;
using LT.SO.Infra.Data.Gerencial.Repository;
using LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Repository;
using LT.SO.Domain.Permissoes.Menu.Interfaces.Repository;
using LT.SO.Infra.Data.Gerencial.Context;
using LT.SO.Infra.Data.Gerencial.UoW;
using LT.SO.Domain.Gerencial.Usuario.Services;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Service;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Repository;

namespace LT.SO.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
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

            #endregion

            #endregion

            #region Infra

            //LogData
            services.AddScoped<LogContext>();
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