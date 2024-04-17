using Microsoft.AspNetCore.Builder;

namespace WebVakt_API.Utility
{
    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
