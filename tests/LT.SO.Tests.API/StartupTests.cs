using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using LT.SO.Services.Api.Middlewares;
using LT.SO.Services.Api.Configurations;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Infra.CrossCutting.Bus;
using LT.SO.Infra.CrossCutting.IoC;
using LT.SO.Infra.CrossCutting.AspNetFilters;
using LT.SO.Infra.CrossCutting.Identity.Data;
using LT.SO.Infra.CrossCutting.Identity.Authorization;
using Microsoft.Data.Sqlite;

namespace LT.SO.Tests.API
{
    public class StartupTests
    {
        public StartupTests(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string SecretKey = "Regua$C0br4nc4&S3cr3t@T0k3nN";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            services.AddDbContext<ApplicationDbContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                options.UseSqlite(connection)
                );

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddOptions();
            services.AddLogging();
            services.AddMvc(options =>
            {
                //options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));

                var policy = new AuthorizationPolicyBuilder()
                                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                    .RequireAuthenticatedUser()
                                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
                options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
                options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionFilter)));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanReadBloqueio", policy => policy.RequireClaim("Bloqueio", "Read"));
                options.AddPolicy("CanWriteBloqueio", policy => policy.RequireClaim("Bloqueio", "Write"));
                options.AddPolicy("CanReadPermissao", policy => policy.RequireClaim("Permissoes", "Read"));
                options.AddPolicy("CanWritePermissao", policy => policy.RequireClaim("Permissoes", "Write"));
                options.AddPolicy("CanReadUsuario", policy => policy.RequireClaim("Usuario", "Read"));
                options.AddPolicy("CanWriteUsuario", policy => policy.RequireClaim("Usuario", "Write"));

                options.AddPolicy("CanReadGrupoAcesso", policy => policy.RequireClaim("GrupoAcesso", "Ler"));
            });

            #region Token Config
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtTokenOptions));

            services.Configure<JwtTokenOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthentication()
                    .AddJwtBearer(cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;

                        cfg.TokenValidationParameters = new TokenValidationParameters()
                        {
                            //ValidIssuer = Configuration["Tokens:Issuer"],
                            ValidateIssuer = true,
                            ValidIssuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)],
                            //ValidAudience = Configuration["Tokens:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)],
                            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = _signingKey,
                            RequireExpirationTime = true,
                            ValidateLifetime = true,

                            ClockSkew = TimeSpan.Zero
                        };

                    });
            #endregion

            services.AddAutoMapper();

            //Registrar todos os DI
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IHttpContextAccessor accessor,
            IUser user)
        {
            app.UseMiddleware<ExceptionHandler>();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
                //c.WithOrigins("www.meusite.com");
            });

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc();

            InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}