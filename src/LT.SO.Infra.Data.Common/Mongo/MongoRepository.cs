using System;
using MongoDB.Driver;

namespace LT.SO.Infra.Data.Common.Mongo
{
    public abstract class MongoRepository<T> where T : class
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;
        private readonly Type typeT = typeof(T);

        public MongoRepository(IMongoDatabase database)
        {
            _database = database;
            _collectionName = typeT.Name;
        }

        protected IMongoCollection<T> Collection
            => _database.GetCollection<T>(_collectionName);
    }
}