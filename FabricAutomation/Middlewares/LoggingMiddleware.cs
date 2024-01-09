namespace FabricAutomation.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;

        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<LoggingMiddleware> logger)
        {
            await next(context);

            logger.LogInformation("Endpoint:{endpoint} | Method:[{method}] | Status Code:{code}\n", context.Request.Path, context.Request.Method, context.Response.StatusCode.ToString());

        }
    }
}
