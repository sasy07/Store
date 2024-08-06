using System.Net;
using System.Text.Json;
using Domain.Exceptions;

namespace Web.Middleware;

public class MiddlewareExceptionHandler(
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
            var option = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var result = HandleServerError(httpContext, e, option);
            result = HandleResult(httpContext, e, result, option);
            await httpContext.Response.WriteAsync(result);
        }
    }

    private static string HandleServerError(HttpContext httpContext, Exception e, JsonSerializerOptions option)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new ApiToReturn(500, e.Message), option);
        return result;
    }

    private string HandleResult(HttpContext httpContext, Exception exception, string result,
        JsonSerializerOptions option)
    {
        switch (exception)
        {
            case NotFoundEntityException notFoundEntityException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new ApiToReturn(404, notFoundEntityException.Messages
                    , notFoundEntityException.Message, exception.Message),option);
                break;

            case BadRequestEntityException badRequestEntityException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new ApiToReturn(400, badRequestEntityException.Messages
                    , badRequestEntityException.Message, exception.Message),option);
                break;

            case ValidationEntityException validationEntityException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new ApiToReturn(400, validationEntityException.Messages
                    , validationEntityException.Message, exception.Message),option);
                break;
        }

        return result;
    }
}