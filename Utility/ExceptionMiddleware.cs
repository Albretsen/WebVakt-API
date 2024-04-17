using Azure.Security.KeyVault.Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebVakt_API.Utility
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IExceptionLogging _exceptionLogging;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IExceptionLogging exceptionLogging)
        {
            _next = next;
            _logger = logger;
            _exceptionLogging = exceptionLogging;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
                await LogExceptionAsync(ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, string info = "")
        {
            var code = HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new { error = exception.Message, stackTrace = exception.StackTrace, info });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private async Task LogExceptionAsync(Exception exception)
        {
            var error = new WebVakt_API.Models.Error
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                DateOccurred = DateTime.UtcNow 
            };

            await _exceptionLogging.LogErrorAsync(error);
        }
    }

}
