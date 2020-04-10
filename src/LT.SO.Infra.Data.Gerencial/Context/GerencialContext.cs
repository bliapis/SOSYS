using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LT.SO.Domain.Gerencial.Usuario.Entities;
using LT.SO.Domain.Permissoes.Menu.Entities;
using LT.SO.Domain.Permissoes.Permissao.Entities;
using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;
using LT.SO.Infra.Data.Gerencial.Mappings;

namespace LT.SO.Infra.Data.Gerencial.Context
{
    public class GerencialContext : DbContext
    {
        public DbSet<TipoPermissaoModel> TiposPermissoes { get; set; }
        public DbSet<PermissaoModel> Permissoes { get; set; }
        public DbSet<GrupoAcessoModel> GruposAcesso { get; set; }
        public DbSet<MenuModel> Menus { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TipoPermissaoModelMapping());
            modelBuilder.ApplyConfiguration(new PermissaoModelMapping());
            modelBuilder.ApplyConfiguration(new GrupoAcessoModelMapping());
            modelBuilder.ApplyConfiguration(new GrupoAcessoPermissaoMapping());
            modelBuilder.ApplyConfiguration(new MenuMapping());
            modelBuilder.ApplyConfiguration(new MenusGruposAcessoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioModelMapping());
            modelBuilder.ApplyConfiguration(new UsuarioGrupoAcessoMapping());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json")
                         //.AddJsonFile("appsettings.Testing.json")
                         .Build();

            //var dbType = config.GetSection("DBType");
            //
            //if (dbType != null && dbType.Value == "InMemory")
            //{
            //    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            //    //var connection = new SqliteConnection("DataSource=:memory:");
            //    //connection.Open();
            //    //optionsBuilder.UseSqlite(connection);
            //}
            //else
            //{
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            //}
        }
    }
}