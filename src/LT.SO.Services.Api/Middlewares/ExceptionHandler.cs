using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LT.SO.Services.Api.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string logId = Convert.ToString(exception.Data["LogId"]);

            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                // TODO: Alterar o objeto abaixo conforme necessidade.
                error = new
                {
                    message = "Ocorreu um erro inesperado, tente novamente mais tarde ou contate nosso suporte.",
                    erroId = logId
                }
            }));
        }
    }
}