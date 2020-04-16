using MongoDB.Driver;

namespace LT.SO.Infra.Data.Common.Mongo
{
    public class Seeder : MongoSeeder<Seeder>
    {
        public Seeder(IMongoDatabase database) : base(database)
        {

        }
    }
}