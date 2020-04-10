using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LT.SO.Infra.Data.Log.Mappings;
using LT.SO.Infra.CrossCutting.Log.Entities;
using Microsoft.Data.Sqlite;

namespace LT.SO.Infra.Data.Log.Context
{
    public class LogContext : DbContext
    {
        public DbSet<LogModel> LogErro { get; set; }
        public DbSet<LogAuditoria> LogAuditoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogModelMapping());
            modelBuilder.ApplyConfiguration(new LogAuditoriaMapping());

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