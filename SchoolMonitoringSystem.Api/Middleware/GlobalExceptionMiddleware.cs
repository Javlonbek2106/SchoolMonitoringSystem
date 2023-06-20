namespace SchoolMonitoringSystem.Api.Middleware
{

    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;


        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);

            }
            catch (Exception e)
            {
                HandleException(httpContext, e.Message);
            }

        }

        public void HandleException(HttpContext context, string message)
        {
            string encodedMessage = Uri.EscapeDataString(message);
            context.Response.Redirect($"/Home/Error?errorMessage={encodedMessage}");
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}



