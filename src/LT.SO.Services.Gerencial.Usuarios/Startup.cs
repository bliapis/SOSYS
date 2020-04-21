using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LT.SO.Domain.Core.Repository;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Gerencial.Usuario.Commands;
using LT.SO.Domain.Gerencial.Usuario.Events;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Repository;
using LT.SO.Infra.Data.Event;
using LT.SO.Infra.Data.Gerencial.Context;
using LT.SO.Infra.Data.Gerencial.Repository;
using LT.SO.Infra.Data.Common.Mongo;
using LT.SO.Infra.CrossCutting.IoC;
using LT.SO.Infra.CrossCutting.Identity.Data;
using LT.SO.Infra.CrossCutting.Identity.Models;
using LT.SO.Infra.CrossCutting.Bus;

namespace LT.SO.Services.Gerencial.Usuarios
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                //options.UseSqlite(connection)
                );

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(6);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddOptions();
            services.AddLogging();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<GerencialContext>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddScoped<IEventRepository<UsuarioCreatedEvent>, EventRepository<UsuarioCreatedEvent>>();
            services.AddScoped<IEventRepository<CreateUsuarioRejectedEvent>, EventRepository<CreateUsuarioRejectedEvent>>();

            services.AddScoped<IHandlerMS<CreateUsuarioCommand>, UsuarioCommandHandler>();
            services.AddScoped<IHandlerMS<UsuarioCreatedEvent>, UsuarioEventHandler>();
            services.AddScoped<IHandlerMS<CreateUsuarioRejectedEvent>, UsuarioEventHandler>();

            //Registrar DI Default
            RegisterServices(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IHttpContextAccessor accessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
            app.UseMvc();

            InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            ServiceBaseInjector.RegisterServices(services, configuration);
        }
    }
}