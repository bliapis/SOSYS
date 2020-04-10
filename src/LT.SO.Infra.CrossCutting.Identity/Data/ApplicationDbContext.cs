using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace LT.SO.Infra.CrossCutting.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
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
            //  optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            //  //var connection = new SqliteConnection("DataSource=:memory:");
            //  //connection.Open();
            //  //optionsBuilder.UseSqlite(connection);
            //}
            //else
            //{
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            //}
        }
    }
}