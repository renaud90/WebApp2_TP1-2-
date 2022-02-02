using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Filters
{
    public class HtppResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;
        private readonly ILogger<HtppResponseExceptionFilter> _logger;

        public HtppResponseExceptionFilter(ILogger<HtppResponseExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is HttpException exception)
                {
                    context.Result = new ObjectResult(exception.Errors)
                    {
                        StatusCode = exception.StatusCode
                    };
                    _logger.LogError(exception, "Failed to handle request: {msg}", exception.Errors);//Important de laisser une trace de l'erreur dans les logs
                }
                else
                {
                    context.Result = new ObjectResult(new ProblemDetails { Title = "Interna server error", Status = 500, Detail = context.Exception.Message })
                    {
                        StatusCode = 500
                    };
                    _logger.LogError(context.Exception, "Internal server error: {msg}", context.Exception.Message); //Important de laisser une trace de l'erreur dans les logs
                }

                context.ExceptionHandled = true;

            }


        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
