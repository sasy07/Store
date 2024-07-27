namespace Web.Middleware;

public class ExceptionHandlerMiddleware(
    IWebHostEnvironment webHostEnvironment,
    ILoggerFactory loggerFactory,
    RequestDelegate next)
{
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
    private readonly ILoggerFactory _loggerFactory = loggerFactory;
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}