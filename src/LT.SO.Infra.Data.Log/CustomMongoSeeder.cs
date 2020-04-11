using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using LT.SO.Infra.CrossCutting.Log.Entities;
using LT.SO.Infra.CrossCutting.Log.Interfaces;
using LT.SO.Infra.Data.Common.Mongo;

namespace LT.SO.Infra.Data.Log.Seed
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ILogRepository _logRepo;
        private readonly ILogAuditoriaRepository _logAuditRepo;

        public CustomMongoSeeder(IMongoDatabase database,
            ILogRepository logRepo,
            ILogAuditoriaRepository logAuditRepo) : base(database)
        {
            _logRepo = logRepo;
            _logAuditRepo = logAuditRepo;
        }

        protected override async Task CustomSeedAsync()
        {
            #region Inicia Logs
            var logs = new List<LogModel>()
            {
                new LogModel()
                {
                    Version = "1.0",
                    Method = "CustomSeedAsync",
                    DateTime = DateTime.Now,
                    Detail = "Base de Logs Inicializada",
                    Application = "LT.SO"
                }
            };

            await Task.WhenAll(logs.Select(l =>
                _logRepo.AddAsync(l)));
            #endregion

            #region Inicia LogsAuditoria
            var logsAuditoria = new List<LogAuditoria>()
            {
                new LogAuditoria("", "Base de Logs de Auditoria Inicializada", "", CrossCutting.Log.Enum.LogSourceEnum.Infra_Data, CrossCutting.Log.Enum.LogTypeEnum.Performance)
            };

            await Task.WhenAll(logsAuditoria.Select(a =>
                _logAuditRepo.AddAsync(a)));
            #endregion
        }
    }
}