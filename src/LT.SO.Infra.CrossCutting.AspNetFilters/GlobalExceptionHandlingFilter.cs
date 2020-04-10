using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LT.SO.Infra.CrossCutting.AspNetFilters
{
    public class GlobalExceptionHandlingFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionHandlingFilter> _logger;
        private readonly IHostingEnvironment _hostingEnviroment;

        public GlobalExceptionHandlingFilter(
            ILogger<GlobalExceptionHandlingFilter> logger,
            IHostingEnvironment hostingEnviroment)
        {
            _logger = logger;
            _hostingEnviroment = hostingEnviroment;
        }

        public void OnException(ExceptionContext context)
        {
            //if (_hostingEnviroment.IsProduction())
            //{
                _logger.LogError(1, context.Exception, context.Exception.Message);
            //}

            //var resultAPI = new [] { "MensagemErro", context.Exception.Message };
            //var result = new ViewResult { ViewName = "Error" };
            //
            //_bus.RaiseEvent(new DomainNotification(result.ToString(), context.Exception.Message));
            //
            //var modelData = new EmptyModelMetadataProvider();
            //result.ViewData = new ViewDataDictionary(modelData, context.ModelState)
            //{
            //    { "MensagemErro", context.Exception.Message }
            //};
            //
            //context.ExceptionHandled = true;
            //context.Result = result;
        }
    }
}