using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using LT.SO.Infra.CrossCutting.Log.Entities;
using LT.SO.Infra.CrossCutting.Log.Enum;
using LT.SO.Infra.CrossCutting.Log.Interfaces;

namespace LT.SO.Infra.CrossCutting.Log.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepo;
        private readonly ILogAuditoriaRepository _logAudit;

        public LogService(
            ILogRepository logRepo,
            ILogAuditoriaRepository logAudit)
        {
            _logRepo = logRepo;
            _logAudit = logAudit;
        }

        public void Save(LogModel model)
        {
            _logRepo.Add(model);
            _logRepo.SaveChanges();
        }

        public Guid Save(Exception ex)
        {
            var model = new LogModel
            {
                Version = "v1.0",
                Application = "LT.SO.Infra.Log",
                Source = "CrossCutting.Log",
                DateTime = DateTime.Now,
                LogDatas = JsonConvert.SerializeObject(new
                {
                    LogDatas = GetData(ex)
                }),
                Detail = JsonConvert.SerializeObject(new { ExceptionMessage = ex.Message, InnerExceptionMessage = ex.InnerException?.Message, DadosException = ex })
            };

            _logRepo.Add(model);
            _logRepo.SaveChanges();

            return model.Id;
        }

        public Guid SaveAudit(string identifier, string message, string detail, LogSourceEnum source, LogTypeEnum type)
        {
            var model = new LogAuditoria(identifier, message, detail, source, type);
            _logAudit.Add(model);
            _logAudit.SaveChanges();

            return model.Id;
        }

        public Guid SaveAudit(LogAuditoria log)
        {
            _logAudit.Add(log);
            _logAudit.SaveChanges();

            return log.Id;
        }

        private static List<LogData> GetData(Exception excpt)
        {
            var retorno = new List<LogData>();

            try
            {
                foreach (var key in excpt.Data.Keys)
                {
                    retorno.Add(new LogData(Convert.ToString(key), Convert.ToString(excpt.Data[key])));
                }
            }
            catch (InvalidOperationException)
            {
                // sem dados disponiveis
            }

            return new List<LogData>();
        }

        public void Dispose()
        {
            _logRepo.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}