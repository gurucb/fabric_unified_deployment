using FabricAutomation.Middlewares;

namespace FabricAutomation.Extensions
{
    public static class ExceptionExtension
    {
        public static IApplicationBuilder UseException(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
