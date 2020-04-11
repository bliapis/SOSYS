using System.Threading.Tasks;
using MongoDB.Driver;
using LT.SO.Infra.CrossCutting.Log.Entities;
using LT.SO.Infra.CrossCutting.Log.Interfaces;
using LT.SO.Infra.Data.Common.Mongo;

namespace LT.SO.Infra.Data.Log.Repository
{
    public class LogAuditoriaRepository : MongoRepository<LogAuditoria>, ILogAuditoriaRepository
    {
        private readonly IMongoDatabase _database;

        public LogAuditoriaRepository(IMongoDatabase database) : base(database, "LogsAuditoria")
        {
            _database = database;
        }

        public async Task AddAsync(LogAuditoria obj)
            => await Collection.InsertOneAsync(obj);
    }
}