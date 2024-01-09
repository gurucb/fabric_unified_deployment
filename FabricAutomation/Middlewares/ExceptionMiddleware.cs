using System.Net;
using System.Text.Json;
using System;

namespace FabricAutomation.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                httpContext.Response.ContentType = "application/json";
                var response = httpContext.Response;

                switch (exception)
                {
                    case ApplicationException ex:
                        if (ex.Message.Contains("Invalid Token"))
                        {
                            response.StatusCode = (int)HttpStatusCode.Forbidden;
                            break;
                        }
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ArgumentNullException ex:
                        if (ex.Message.Contains("not found"))
                        {
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;
                        }
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var logMessage = $"""{exception}, "ErrorHandlingMiddleware" | [Method]: "HandleExceptionAsync" | Error: {exception.Message} Headers: {httpContext.Request.Headers}. Path: {httpContext.Request.Path}. Body: {httpContext.Request.Body}""";
                _logger.LogError(logMessage);

                var result = JsonSerializer.Serialize(exception.Message);
                await httpContext.Response.WriteAsync(result);
            }
        }
    }
}
