using System.Net;
using quizon.Exceptions;

namespace quizon.Middleware
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);


    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occurred.");

            ExceptionResponse response;
            if (exception is HttpResponseException httpException)
            {
                response = new ExceptionResponse(httpException.Status, GetDescription((int)httpException.Status, httpException.Description));
            }
            else
            {
                response = exception switch
                {
                    ApplicationException _ => new ExceptionResponse(HttpStatusCode.BadRequest, "Application exception occurred."),
                    KeyNotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, "The request key not found."),
                    UnauthorizedAccessException _ => new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized."),
                    _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
                };
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }

        private string GetDescription(int code, string Description)
        {
            if (!string.IsNullOrWhiteSpace(Description))
            {
                return Description.Trim();
            }

            return code switch
            {
                400 => "Application exception occurred.",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Element not found",
                500 => "Internal server error. Please retry later.",
                _ => string.Empty,
            };
        }
    }
}