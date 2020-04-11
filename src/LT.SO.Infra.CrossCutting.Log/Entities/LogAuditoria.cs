using System;
using LT.SO.Domain.Core.Models;
using LT.SO.Infra.CrossCutting.Log.Enum;

namespace LT.SO.Infra.CrossCutting.Log.Entities
{
    public class LogAuditoria //: Entity<LogAuditoria>
    {
        public LogAuditoria() {}

        public LogAuditoria(string identifier, string message, string detail, LogSourceEnum source, LogTypeEnum type)
        {
            Id = Guid.NewGuid();

            Identifier = identifier;
            Message = message;
            Detail = detail;
            Source = source.ToString();
            Type = type.ToString();
            Data = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string Identifier { get; private set; }
        public string Message { get; private set; }
        public string Detail { get; private set; }
        public string Source { get; private set; }
        public string Type { get; private set; }
        public DateTime? Data { get; private set; }
        public string User { get; private set; }
        public string Hostname { get; private set; }
        public string Url { get; private set; }
        public string Controller { get; private set; }
        public string Method { get; private set; }
        public int StatusCode { get; private set; }
        public string Cookies { get; private set; }
        public string ServerVariables { get; private set; }

        //public override bool IsValid()
        //{
        //    throw new NotImplementedException();
        //}

        public static class LogAuditoriaFactory
        {
            public static LogAuditoria NewLogAuditoriaFull(string identifier, string message, string detail, LogSourceEnum source, LogTypeEnum type, string user, string hostname, string url, string controller, string method, int statusCode, string cookies, string serverVariables)
            {
                var logAudit = new LogAuditoria()
                {
                    Id = Guid.NewGuid(),
                    Data = DateTime.Now,

                    Identifier = identifier,
                    Message = message,
                    Detail = detail,
                    Source = source.ToString(),
                    Type = type.ToString(),
                    User = user,
                    Hostname = hostname,
                    Url = url,
                    Controller = controller,
                    Method = method,
                    StatusCode = statusCode,
                    Cookies = cookies,
                    ServerVariables = serverVariables
                };

                return logAudit;
            }
        }
    }
}