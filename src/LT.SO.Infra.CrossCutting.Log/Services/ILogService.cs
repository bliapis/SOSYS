using LT.SO.Infra.CrossCutting.Log.Entities;
using LT.SO.Infra.CrossCutting.Log.Enum;
using System;
using System.Threading.Tasks;

namespace LT.SO.Infra.CrossCutting.Log.Services
{
    public interface ILogService : IDisposable
    {
        Task SaveAsync(LogModel model);
        Task<Guid> SaveAsync(Exception ex);
        Task<Guid> SaveAuditAsync(string identifier, string message, string detail, LogSourceEnum source, LogTypeEnum type);
        Task<Guid> SaveAuditAsync(LogAuditoria log);
    }
}