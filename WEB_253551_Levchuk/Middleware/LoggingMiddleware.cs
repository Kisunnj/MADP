using Serilog;
using System.Diagnostics;

namespace WEB_253551_Levchuk.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            var requestPath = context.Request.Path;
            var requestMethod = context.Request.Method;

            try
            {
                await _next(context);
                
                sw.Stop();
                var statusCode = context.Response.StatusCode;
                var elapsed = sw.ElapsedMilliseconds;

                // Логирование успешных запросов
                if (statusCode >= 200 && statusCode < 400)
                {
                    _logger.LogInformation(
                        "HTTP {Method} {Path} responded {StatusCode} in {Elapsed}ms",
                        requestMethod, requestPath, statusCode, elapsed);
                }
                // Логирование ошибок клиента (4xx)
                else if (statusCode >= 400 && statusCode < 500)
                {
                    _logger.LogWarning(
                        "HTTP {Method} {Path} responded {StatusCode} in {Elapsed}ms",
                        requestMethod, requestPath, statusCode, elapsed);
                }
                // Логирование ошибок сервера (5xx)
                else if (statusCode >= 500)
                {
                    _logger.LogError(
                        "HTTP {Method} {Path} responded {StatusCode} in {Elapsed}ms",
                        requestMethod, requestPath, statusCode, elapsed);
                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                var elapsed = sw.ElapsedMilliseconds;

                // Логирование исключений
                _logger.LogError(ex,
                    "Unhandled exception occurred while processing HTTP {Method} {Path} after {Elapsed}ms",
                    requestMethod, requestPath, elapsed);

                // Повторный выброс исключения для обработки другими middleware
                throw;
            }
        }
    }

    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}

