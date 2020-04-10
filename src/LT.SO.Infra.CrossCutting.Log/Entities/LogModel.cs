using LT.SO.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace LT.SO.Infra.CrossCutting.Log.Entities
{
    public class LogModel : Entity<LogModel>
    {
        public LogModel()
        {
            Id = Guid.NewGuid();
        }

        public string Version { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public string Severity { get; set; }
        public string User { get; set; }
        public string Type { get; set; }
        public DateTime? DateTime { get; set; }
        public int? StatusCode { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string Hostname { get; set; }
        public string Detail { get; set; }
        public string Application { get; set; }
        public string LogQueryStrings { get; set; }
        public string LogForms { get; set; }
        public string LogCookies { get; set; }
        public string LogServerVariables { get; set; }
        public string LogDatas { get; set; }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}