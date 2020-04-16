using System.Threading.Tasks;
using e = LT.SO.Domain.Core.Events;
using LT.SO.Domain.Core.Repository;
using LT.SO.Infra.Data.Common.Mongo;
using MongoDB.Driver;

namespace LT.SO.Infra.Data.Event
{
    public class EventRepository<T> : MongoRepository<T>, IEventRepository<T> where T : e.Event
    {
        private readonly IMongoDatabase _database;

        public EventRepository(IMongoDatabase database) : base(database)
        {
            _database = database;
            
        }

        public async Task AddAsync(T obj)
         => await Collection.InsertOneAsync(obj);
    }
}