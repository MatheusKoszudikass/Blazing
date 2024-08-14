using Blazing.Api.Erros;
using Blazing.Api.Erros.ControllerExceptions;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Produtos;
using System.Text.Json;

namespace Blazing.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                await DetermineException(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, DomainException ex, string message)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";

            var response = _env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace?.ToString())
                : new ApiException(context.Response.StatusCode.ToString(), message, null);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }

        private async Task DetermineException(HttpContext context, DomainException ex)
        {
            if (ex is ProductExceptions.ProductAlreadyExistsException ||
                  ex is ProductExceptions.IdentityProductInvalidException ||
                  ex is ProductExceptions.ProductInvalidException ||
                  ex is ProductExceptions.ProductNotFoundException)
            {
                await HandleProductException(context, ex);
            }
            else
            {
                // You can add handling for other types of exceptions here if needed.
                await HandleProductException(context, ex);
            }
        }

        private async Task HandleProductException(HttpContext context, DomainException ex)
        {
            string? message = string.Empty;

            switch (ex)
            {
                case ProductExceptions.ProductAlreadyExistsException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    message = ex.Message;
                    break;
                case ProductExceptions.ProductNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    message = ex.Message;
                    break;
                case ProductExceptions.IdentityProductInvalidException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    message = ex.Message;
                    break;
                case ProductExceptions.ProductInvalidException:
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    message = ex.Message;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            await HandleExceptionAsync(context, ex, message);
        }

    }
}
