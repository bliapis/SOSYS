using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace LT.SO.Infra.Data.Common.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase Database;

        public MongoSeeder(IMongoDatabase database)
        {
            Database = database;
        }

        public async Task SeedAsync()
        {
            var collectionCursor = await Database.ListCollectionsAsync();
            var collections = await collectionCursor.ToListAsync();

            if (collections.Any())
                return;

            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}