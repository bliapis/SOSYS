using LT.SO.Infra.CrossCutting.Log.Entities;
using LT.SO.Infra.CrossCutting.Log.Enum;
using System;

namespace LT.SO.Infra.CrossCutting.Log.Interfaces
{
    public interface ILogService : IDisposable
    {
        void Save(LogModel model);
        Guid Save(Exception ex);
        Guid SaveAudit(string identifier, string message, string detail, LogSourceEnum source, LogTypeEnum type);
        Guid SaveAudit(LogAuditoria log);
    }
}