using System.Threading.Tasks;
using LT.SO.Infra.CrossCutting.Log.Entities;
using LT.SO.Infra.CrossCutting.Log.Interfaces;
using LT.SO.Infra.Data.Common.Mongo;
using MongoDB.Driver;

namespace LT.SO.Infra.Data.Log.Repository
{
    public class LogRepository : MongoRepository<LogModel>, ILogRepository
    {
        private readonly IMongoDatabase _database;

        public LogRepository(IMongoDatabase database) : base(database)
        {
            _database = database;
        }

        public async Task AddAsync(LogModel obj)
            => await Collection.InsertOneAsync(obj);
    }
}