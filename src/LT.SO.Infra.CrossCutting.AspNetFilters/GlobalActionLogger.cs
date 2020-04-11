using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using LT.SO.Infra.CrossCutting.Log.Entities;
using LT.SO.Infra.CrossCutting.Log.Services;

namespace LT.SO.Infra.CrossCutting.AspNetFilters
{
    public class GlobalActionLogger : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionHandlingFilter> _logger;
        private readonly IHostingEnvironment _hostingEnviroment;
        private readonly ILogService _logService;

        public GlobalActionLogger(
            ILogger<GlobalExceptionHandlingFilter> logger,
            IHostingEnvironment hostingEnviroment,
            ILogService logService)
        {
            _logger = logger;
            _hostingEnviroment = hostingEnviroment;
            _logService = logService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_hostingEnviroment.IsDevelopment())
            {
                var data = new
                {
                    Version = "v1.0",
                    User = context.HttpContext.User.Identity.Name,
                    IP = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Hostname = context.HttpContext.Request.Host.ToString(),
                    AreaAccessed = context.HttpContext.Request.GetDisplayUrl(),
                    Action = context.ActionDescriptor.DisplayName,
                    TimeStamp = DateTime.Now
                };

                _logger.LogInformation(1, data.ToString(), "Log de Auditoria");
            }

            //if (_hostingEnviroment.IsProduction())
            //{
            //    var message = new CreateMessage
            //    {
            //        Version = "v1.0",
            //        Application = "LT.SO",
            //        Source = "GlobalActionLoggerFilter",
            //        User = context.HttpContext.User.Identity.Name,
            //        Hostname = context.HttpContext.Request.Host.Host,
            //        Url = context.HttpContext.Request.GetDisplayUrl(),
            //        DateTime = DateTime.Now,
            //        Method = context.HttpContext.Request.Method,
            //        StatusCode = context.HttpContext.Response.StatusCode,
            //        Cookies = context.HttpContext.Request?.Cookies?.Keys.Select(k => new Item(k, context.HttpContext.Request.Cookies[k])).ToList(),
            //        Form = Form(context.HttpContext),
            //        ServerVariables = context.HttpContext.Request?.Headers?.Keys.Select(k => new Item(k, context.HttpContext.Request.Headers[k])).ToList(),
            //        QueryString = context.HttpContext.Request?.Query?.Keys.Select(k => new Item(k, context.HttpContext.Request.Query[k])).ToList(),
            //        Data = context.Exception?.ToDataList(),
            //        Detail = JsonConvert.SerializeObject(new { DadoExtra = "Dados a mais", DadoInfo = "Pode ser um Json" })
            //    };
            //
            //    //TODO: Ao invés de salvar no servidor do elmah, enviar isso para um local de logs meu
            //    //var client = ElmahioAPI.Create("61590993437a4366b3f78b510dfb9ef2");
            //    //client.Messages.Create(new Guid("3ea27d04-1e19-4709-889b-46f1d063c364").ToString(), message);
            //}
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnException(ExceptionContext context)
        {
            var message = new LogModel
            {
                Version = "v1.0",
                Application = "LT.SO",
                Source = "GlobalActionLoggerFilter",
                User = string.Format("Usuario: {0} - UserId: {1}",
                                context.HttpContext.User.Claims.Where(c => c.Type.Contains("givenname")).FirstOrDefault()?.Value,
                                context.HttpContext.User.Claims.Where(c => c.Type.Contains("nameidentifier")).FirstOrDefault()?.Value),
                Hostname = context.HttpContext.Request.Host.Host,
                Url = context.HttpContext.Request.GetDisplayUrl(),
                DateTime = DateTime.Now,
                Method = context.HttpContext.Request.Method,
                StatusCode = context.HttpContext.Response.StatusCode,
                LogCookies = JsonConvert.SerializeObject(new
                {
                    Cookies = context.HttpContext.Request?.Cookies?.Keys.Select(k => new LogCookie(k, (context.HttpContext.Request.Cookies[k] == null ? string.Empty : context.HttpContext.Request.Cookies[k]))).ToList()
                }),
                LogForms = JsonConvert.SerializeObject(new
                {
                    Forms = Form(context.HttpContext).ToList()
                }),
                LogServerVariables = JsonConvert.SerializeObject(new
                {
                    ServerVariables = context.HttpContext.Request?.Headers?.Keys.Select(k => new LogServerVariable(k, context.HttpContext.Request.Headers[k])).ToList()
                }),
                LogQueryStrings = JsonConvert.SerializeObject(new
                    {
                        QueryStrings = context.HttpContext.Request?.Query?.Keys.Select(k => new LogQueryString(k, context.HttpContext.Request.Query[k])).ToList()
                    }),
                //LogDatas = context.Exception?.ToDataList().Select(i => new LogData((i.Key == null ? "" : i.Key), (i.Value == null ? "" : i.Value))).ToList(),
                LogDatas = JsonConvert.SerializeObject(new
                {
                    LogDatas = GetData(context.Exception)
                }),
                Detail = JsonConvert.SerializeObject(new { ExceptionMessage = context.Exception?.Message, InnerExceptionMessage = context.Exception?.InnerException?.Message, DadosException = context.Exception })
            };

            //Salvar no Banco
            _logService.SaveAsync(message);

            //Adiciono o ID do Log na Exception
            context.Exception.Data.Add("LogId", message.Id);
        }

        private static List<LogForm> Form(HttpContext httpContext)
        {
            try
            {
                return httpContext.Request?.Form?.Keys.Select(k => new LogForm(k, httpContext.Request.Form[k])).ToList();
            }
            catch (InvalidOperationException)
            {
                // Request not a form POST or similar
            }

            return new List<LogForm>();
        }

        private static List<LogData> GetData(Exception excpt)
        {
            var retorno = new List<LogData>();

            try
            {
                foreach(var key in excpt.Data.Keys)
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
    }
}