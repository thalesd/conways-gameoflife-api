using System.Net;
using System.Text.Json;

namespace Conways_GameOfLife_API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Proceed to next middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new ErrorResponse
                {
                    Error = "Internal server error",
                    #if DEBUG
                    Detail = ex.Message
                    #endif
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }

        private class ErrorResponse
        {
            public string Error { get; set; } = string.Empty;
            public string? Detail { get; set; }
        }
    }
}
