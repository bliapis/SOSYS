using LT.SO.Infra.CrossCutting.Log.Entities;
using LT.SO.Infra.CrossCutting.Log.Interfaces;
using LT.SO.Infra.Data.Log.Context;

namespace LT.SO.Infra.Data.Log.Repository
{
    public class LogRepository : Repository<LogModel>, ILogRepository
    {
        public LogRepository(LogContext context) : base(context)
        {
        }
    }
}