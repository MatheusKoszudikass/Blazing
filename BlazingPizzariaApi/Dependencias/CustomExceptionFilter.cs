using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BlazingPizza.Api.Dependencias
{
    //public class CustomExceptionFilter : IExceptionFilter
    //{
    //    private readonly InjectServicesApi _injectServices;

    //    public CustomExceptionFilter(InjectServicesApi injectServices)
    //    {
    //        _injectServices = injectServices;
    //    }

    //    public void OnException(ExceptionContext executionContext)
    //    {
    //        _injectServices._logger.LogError(executionContext.Exception, "Ocorreu uma exceção não tratada");

    //        var problemDetails = new ProblemDetails
    //        {
    //            Status = StatusCodes.Status500InternalServerError,
    //            Title = "Ocorreu uma exceção não tratada",
    //            Detail = executionContext.Exception.Message,
    //            Instance = executionContext.HttpContext.Request.Path,
                
    //        };

    //        executionContext.Result = new ObjectResult(problemDetails)
    //        {
    //            StatusCode = StatusCodes.Status500InternalServerError
    //        };

    //        executionContext.ExceptionHandled = true;   
    //    }
    //}
}
