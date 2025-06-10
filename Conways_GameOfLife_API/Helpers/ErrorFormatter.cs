using Microsoft.AspNetCore.Mvc;

namespace Conways_GameOfLife_API.Helpers
{
    public class ErrorFormatter
    {
        public static IActionResult Format(string message, string? detail = null, int statusCode = 400)
        {
            var body = new
            {
                error = message,
                detail = detail
            };

            return new ObjectResult(body)
            {
                StatusCode = statusCode
            };
        }
    }
}
