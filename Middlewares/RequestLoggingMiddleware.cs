namespace ProductCatalog.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"Method: {context.Request.Method}; Path: {context.Request.Path}");
            await _next(context);
        }
    }

    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLoggingHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
