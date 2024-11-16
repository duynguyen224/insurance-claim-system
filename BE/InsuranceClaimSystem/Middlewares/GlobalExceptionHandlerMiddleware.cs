using InsuranceClaimSystem.DTOs;
using System.Text.Json;

namespace InsuranceClaimSystem.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errors = new Dictionary<string, List<string>>
                                {
                                    { "Exceptions", new List<string> { exception.Message } }
                                };

            var response = new ApiResponse<object>.Builder()
                .SetStatusCode(StatusCodes.Status500InternalServerError)
                .SetMessage("An unexpected error occurred.")
                .SetErrors(errors)
                .Build();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
