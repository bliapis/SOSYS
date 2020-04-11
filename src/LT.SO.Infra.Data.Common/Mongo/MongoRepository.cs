using MongoDB.Driver;

namespace LT.SO.Infra.Data.Common.Mongo
{
    public abstract class MongoRepository<T> where T : class
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        public MongoRepository(IMongoDatabase database,
            string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
        }

        protected IMongoCollection<T> Collection
            => _database.GetCollection<T>(_collectionName);
    }
}