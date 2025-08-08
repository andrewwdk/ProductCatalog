namespace ProductCatalog.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = 500;
                await response.WriteAsync(exception.Message);
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
