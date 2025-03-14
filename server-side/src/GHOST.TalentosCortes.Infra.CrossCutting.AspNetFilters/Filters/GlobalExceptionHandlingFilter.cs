using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace GHOST.TalentosCortes.Infra.CrossCutting.AspNetFilters.Filters
{
    public class GlobalExceptionHandlingFilter : IExceptionFilter
    {
        readonly ILogger<GlobalExceptionHandlingFilter> _logger;
        readonly IHostingEnvironment _hostingEnviroment;

        public GlobalExceptionHandlingFilter(ILogger<GlobalExceptionHandlingFilter> logger,
            IHostingEnvironment hostingEnviroment)
        {
            _logger = logger;
            _hostingEnviroment = hostingEnviroment;
        }

        public void OnException(ExceptionContext context)
        {
            if (_hostingEnviroment.IsProduction())
            {
                _logger.LogError(1, context.Exception, context.Exception.Message);
            }

            var result = new ViewResult { ViewName = "Error" };
            var modelData = new EmptyModelMetadataProvider();
            result.ViewData = new ViewDataDictionary(modelData, context.ModelState)
            {
                {"MensagemErro", context.Exception.Message}
            };

            context.ExceptionHandled = true;
            context.Result = result;
        }
    }
}


