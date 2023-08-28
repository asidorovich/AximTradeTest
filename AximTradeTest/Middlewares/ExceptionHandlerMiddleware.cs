using AximTradeTest.Models.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace AximTradeTest.Middlewares;

public class ExceptionHandlerMiddleware
{
    // Enrich is a custom extension method that enriches the Serilog functionality - you may ignore it
    //private static readonly ILogger Logger = Log.ForContext(MethodBase.GetCurrentMethod()?.DeclaringType).Enrich();

    private readonly RequestDelegate _next;

    private (HttpStatusCode code, string message) GetResponse(Exception exception)
    {
        var exceptionModel = new ExceptionModel
        {
            Id = Guid.NewGuid(),
            Type = exception.GetType().ToString()
        };

        if(exception is SecurityException)
        {
            exceptionModel.Data.Add("Message", exception.Message);
        }
        else
        {
            exceptionModel.Data.Add("Message", $"Internal server error ID = 638136064187111634");
        }

        return (HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(exceptionModel));
    }

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
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
            // log the error
            //Logger.Error(exception, "error during executing {Context}", context.Request.Path.Value);
            var response = context.Response;
            response.ContentType = "application/json";

            // get the response code and message
            var (status, message) = GetResponse(exception);
            response.StatusCode = (int)status;
            await response.WriteAsync(message);
        }
    }
}
