using Models.ValueObjects;
using System.Net;
using System.Text.Json;

namespace FluentValidationApp;

public sealed class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public ExceptionHandler(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandelException(context, ex);
        }
    }

    private Task HandelException(HttpContext context, Exception exception)
    {
        string errorMessage = _env.IsProduction() ? "Internal Server Error" : "Exception: " + exception.Message;
        var error = Errors.General.InternalServerError(errorMessage);
        var apiResult = ApiResult.Error(new List<Error>() { error });
        var result = JsonSerializer.Serialize(apiResult);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(result);
    }
}

