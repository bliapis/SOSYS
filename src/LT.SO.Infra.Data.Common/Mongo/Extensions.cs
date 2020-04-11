using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LT.SO.Infra.Data.Common.Mongo
{
    public static class Extensions
    {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoOptions>(config.GetSection("mongo"));
            services.AddSingleton<MongoClient>(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();

                return new MongoClient(options.Value.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();
                var client = c.GetService<MongoClient>();

                return client.GetDatabase(options.Value.Database);
            });

            services.AddScoped<IDatabaseInitializer, MongoInitializer>();
            services.AddScoped<IDatabaseSeeder, MongoSeeder>();
        }
    }
}