using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using LT.SO.Infra.CrossCutting.Log.Entities;
using LT.SO.Infra.CrossCutting.Log.Services;

namespace LT.SO.Infra.CrossCutting.AspNetFilters
{
    public class GlobalActionFilter : IActionFilter
    {
        private readonly IHostingEnvironment _hostingEnviroment;
        private readonly ILogService _logService;


        public GlobalActionFilter(
            IHostingEnvironment hostingEnviroment,
            ILogService logService)
        {
            _hostingEnviroment = hostingEnviroment;
            _logService = logService;
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            //Para produção descomentar esse if, senão vamos ficar logando dev sem necessidade.
            //if (_hostingEnviroment.IsProduction())
            //{
                var logAudit = LogAuditoria.LogAuditoriaFactory.NewLogAuditoriaFull(
                    context.HttpContext.TraceIdentifier,
                    "Iniciada requisição na API",
                    JsonConvert.SerializeObject(context.ActionArguments),
                    Log.Enum.LogSourceEnum.Api,
                    Log.Enum.LogTypeEnum.Navegacao,
                    string.Format("Usuario: {0} - UserId: {1}",
                                (context.HttpContext.User.Claims.Where(c => c.Type.Contains("givenname")).FirstOrDefault()
                                != null ? context.HttpContext.User.Claims.Where(c => c.Type.Contains("givenname")).FirstOrDefault().Value
                                : string.Empty),
                                (context.HttpContext.User.Claims.Where(c => c.Type.Contains("nameidentifier")).FirstOrDefault()
                                != null ? context.HttpContext.User.Claims.Where(c => c.Type.Contains("nameidentifier")).FirstOrDefault().Value
                                : string.Empty)),
                    context.HttpContext.Request.Host.Host,
                    context.HttpContext.Request.GetDisplayUrl(),
                    context.Controller.ToString(),
                    context.HttpContext.Request.Method,
                    context.HttpContext.Response.StatusCode,
                    JsonConvert.SerializeObject(new
                    {
                        Cookies = context.HttpContext.Request?.Cookies?.Keys.Select(k => new LogCookie(k, (context.HttpContext.Request.Cookies[k] == null ? string.Empty : context.HttpContext.Request.Cookies[k]))).ToList()
                    }),
                    JsonConvert.SerializeObject(new
                    {
                        ServerVariables = context.HttpContext.Request?.Headers?.Keys.Select(k => new LogServerVariable(k, context.HttpContext.Request.Headers[k])).ToList()
                    })
                );

                await _logService.SaveAuditAsync(logAudit);
            //}
        }

        public async void OnActionExecuted(ActionExecutedContext context)
        {
            //Para produção descomentar esse if, senão vamos ficar logando dev sem necessidade.
            //if (_hostingEnviroment.IsProduction())
            //{
                var logAudit = LogAuditoria.LogAuditoriaFactory.NewLogAuditoriaFull(
                    context.HttpContext.TraceIdentifier,
                    "Finalizada requisição API",
                    "",
                    Log.Enum.LogSourceEnum.Api,
                    Log.Enum.LogTypeEnum.Navegacao,
                    string.Format("Usuario: {0} - UserId: {1}",
                                    (context.HttpContext.User.Claims.Where(c => c.Type.Contains("givenname")).FirstOrDefault()
                                    != null ? context.HttpContext.User.Claims.Where(c => c.Type.Contains("givenname")).FirstOrDefault().Value
                                    : string.Empty),
                                    (context.HttpContext.User.Claims.Where(c => c.Type.Contains("nameidentifier")).FirstOrDefault()
                                    != null ? context.HttpContext.User.Claims.Where(c => c.Type.Contains("nameidentifier")).FirstOrDefault().Value
                                    : string.Empty)),
                    context.HttpContext.Request.Host.Host,
                    context.HttpContext.Request.GetDisplayUrl(),
                    context.Controller.ToString(),
                    context.HttpContext.Request.Method,
                    context.HttpContext.Response.StatusCode,
                    JsonConvert.SerializeObject(new
                    {
                        Cookies = context.HttpContext.Request?.Cookies?.Keys.Select(k => new LogCookie(k, (context.HttpContext.Request.Cookies[k] == null ? string.Empty : context.HttpContext.Request.Cookies[k]))).ToList()
                    }),
                    JsonConvert.SerializeObject(new
                    {
                        ServerVariables = context.HttpContext.Request?.Headers?.Keys.Select(k => new LogServerVariable(k, context.HttpContext.Request.Headers[k])).ToList()
                    })
                );

                await _logService.SaveAuditAsync(logAudit);
            //}
        }
    }
}