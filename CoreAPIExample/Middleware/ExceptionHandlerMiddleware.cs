using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CoreAPIExample.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                _logger.LogDebug(JsonConvert.SerializeObject(httpContext.ToString()));
                await _next(httpContext).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var path = httpContext.Request.Path;
                _logger.LogError(ex, "Middleware Exception Handler");
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(ex.ToString()));
            }

            return;
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
