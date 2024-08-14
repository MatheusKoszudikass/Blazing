namespace Blazing.Api.Erros
{
    public class ApiException(string? statusCode, string? message, string? details) : Exception(message)
    {
        public string? StatusCode { get; set; } = statusCode;
        public string? Details { get; set; } = details;
    }
}
