using FabricAutomation.Middlewares;

namespace FabricAutomation.Extensions
{
    public static class LoggingExtension
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingMiddleware>();
        }
    }
}