using System.Runtime.InteropServices;
using System.Text.Json;
using Blazing.Api.Erros;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Category;
using Blazing.Domain.Exceptions.Product;
using Blazing.Domain.Exceptions.User;
using Serilog.Context;

namespace Blazing.Api.Middleware
{
    #region ExceptionsControllers

    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
    {
        /// <summary>
        /// Logs information about the HTTP request and the system where the API is running.
        /// </summary>
        /// <param name="context">The HTTP context of the request.</param>
        private static void LogsInformation(HttpContext context)
        {
            //Client IP information.
            var ipAddress = context.Connection.RemoteIpAddress.ToString();
            var port = context.Connection.RemotePort.ToString();

            //Information about the operating system where the API is running.
            var osVersion = RuntimeInformation.OSDescription;
            var processorCount = Environment.ProcessorCount.ToString();

            //Browser header information.
            var headers = context.Request.Headers;
            var method = context.Request.Method;
            var body = context.Request.Body;

            //Adding information to the log.
            LogContext.PushProperty("IpAddress", ipAddress);
            LogContext.PushProperty("Port", port);
            LogContext.PushProperty("Operating System", osVersion);
            LogContext.PushProperty("Core Processors", processorCount);
            LogContext.PushProperty("Method", method);
            LogContext.PushProperty("Body", body);
            LogContext.PushProperty("Headers", headers);
        }

        /// <summary>
        /// Middleware that handles exceptions and logs information about the request and exception.
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                LogsInformation(context);
                await next(context);
            }
            catch (DomainException ex)
            {
                await DetermineException(context, ex);
            }
        }

        /// <summary>
        /// Represents the options for serializing JSON.
        /// </summary>
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        /// <summary>
        /// Handles exceptions and writes the response as JSON.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="ex">The domain exception.</param>
        /// <param name="message">The error message.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task HandleExceptionAsync(HttpContext context, DomainException ex, string message)
        {
            context.Response.ContentType = "application/json";

            var response = env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace)
                : new ApiException(context.Response.StatusCode.ToString(), message, null);


            var json = JsonSerializer.Serialize(response, JsonSerializerOptions);
            await context.Response.WriteAsync(json);
        }

        /// <summary>
        /// Determines the type of exception and calls the appropriate handler.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="ex">The domain exception.</param>
        private async Task DetermineException(HttpContext context, DomainException ex)
        {

            switch (ex)
            {
                case DomainException.NotFoundException:
                case DomainException.IdentityInvalidException:
                    await HandleDomainException(context, ex);
                    break;
                case ProductExceptions.ProductAlreadyExistsException:
                case ProductExceptions.IdentityProductInvalidException:
                case ProductExceptions.ProductInvalidException:
                case ProductExceptions.ProductNotFoundException:
                    await HandleProductException(context, ex);
                    break;
                case CategoryExceptions.CategoryAlreadyExistsException:
                case CategoryExceptions.CategoryNotFoundException:
                case CategoryExceptions.CategoryInvalidExceptions:
                    await HandleCategoryException(context, ex);
                    break;
                case UserException.UserAlreadyExistsException:
                case UserException.UserLockedOutException:
                case UserException.IdentityAddUserException:
                case UserException.UserInvalidException:
                case UserException.UserNotFoundException:
                    await HandleUserException(context, ex);
                    break;
                default:
                    await HandleUserException(context, ex);
                    break;
            }


        }

        /// <summary>
        /// Handles a domain exception by setting the appropriate HTTP status code and logging the exception.
        /// </summary>
        /// <param name="context">The HTTP context of the request.</param>
        /// <param name="ex">The domain exception to handle.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task HandleDomainException(HttpContext context, DomainException ex)
        {
            var message = string.Empty;
            switch (ex)
            {
                case DomainException.NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    message = ex.Message;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    break;

                case DomainException.IdentityInvalidException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    message = ex.Message;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    break;
            }

            await HandleExceptionAsync(context, ex, message);
        }

        /// <summary>
        /// Handles a product exception by setting the appropriate HTTP status code and logging the error message.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="ex">The product exception.</param>
        /// <returns>A task representing the asynchronous operation.</returns
        private async Task HandleProductException(HttpContext context, DomainException ex)
        {
            string message;

            switch (ex)
            {
                case ProductExceptions.ProductAlreadyExistsException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    message = ex.Message;
                    break;
                case ProductExceptions.ProductNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    message = ex.Message;
                    break;
                case ProductExceptions.IdentityProductInvalidException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    message = ex.Message;
                    break;
                case ProductExceptions.ProductInvalidException:
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    message = ex.Message;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    message = "An unexpected error occurred.";
                    break;
            }

            await HandleExceptionAsync(context, ex, message);
        }

        /// <summary>
        ///     Handles a category exception by setting the appropriate HTTP status code and logging the warning message.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="ex">The category exception.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task HandleCategoryException(HttpContext context, DomainException ex)
        {
            string message;

            switch (ex)
            {
                case CategoryExceptions.CategoryAlreadyExistsException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    message = ex.Message;
                    break;
                case CategoryExceptions.CategoryNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    message = ex.Message;
                    break;
                case CategoryExceptions.CategoryInvalidExceptions:
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    message = ex.Message;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    message = "An unexpected error occurred.";
                    break;
            }

            await HandleExceptionAsync(context, ex, message);
        }


        /// <summary>
        /// Handles a user exception by setting the appropriate HTTP status code and logging a warning message.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="ex">The user exception.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task HandleUserException(HttpContext context, DomainException ex)
        {
            var message = string.Empty;
            switch (ex)
            {
                case UserException.UserAlreadyExistsException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    message = ex.Message;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    break;
                case UserException.UserLockedOutException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    message = ex.Message;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    break;
                case UserException.UserInvalidException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    message = ex.Message;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    break;

                case UserException.UserNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    message = ex.Message;
                    logger.LogWarning("Ocorreu um aviso: {ErrorMessage}", ex.Message);
                    break;
            }

            await HandleExceptionAsync(context, ex, message);
        }
    }
    #endregion
}