using LT.SO.Infra.Data.Log.Context;
using LT.SO.Infra.CrossCutting.Log.Entities;
using LT.SO.Infra.CrossCutting.Log.Interfaces;

namespace LT.SO.Infra.Data.Log.Repository
{
    public class LogAuditoriaRepository : Repository<LogAuditoria>, ILogAuditoriaRepository
    {
        public LogAuditoriaRepository(LogContext context) : base(context)
        {
        }
    }
}