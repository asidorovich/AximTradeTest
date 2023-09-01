using AximTradeTest.Models.Exceptions;
using Serilog.Context;
using System.Net;
using System.Text.Json;

namespace AximTradeTest.Middlewares;

public class ExceptionHandlerMiddleware
{
    
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            // get the response code and message
            var exceptionModel = GetException(exception);

            LogError(exception, exceptionModel, context);

            var httpResponse = context.Response;
            httpResponse.ContentType = "application/json";
            httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = JsonSerializer.Serialize(exceptionModel, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await httpResponse.WriteAsync(response);
        }
    }

    private ExceptionModel GetException(Exception exception)
    {
        var rnd = new Random();
        long num = rnd.NextInt64();

        var exceptionModel = new ExceptionModel
        {
            Id = num
        };

        string? type;
        string? message;
        object? dataObject = null;
        
        if (exception is SecurityException)
        {
            type = ((SecurityException)exception).Name;
            message = exception.Message;
            dataObject = ((SecurityException)exception).Data;
        }
        else
        {
            type = exception.GetType().Name;
            message = $"Internal server error ID = {num}";
        }

        exceptionModel.Type = type;
        exceptionModel.Data.Add("Message", message);
        exceptionModel.DataObject = dataObject;

        return exceptionModel;
    }

    private void LogError(Exception exception, ExceptionModel exceptionModel, HttpContext context)
    {
        object? data = null;

        if(exceptionModel.DataObject != null)
        {
            data = JsonSerializer.Serialize(exceptionModel.DataObject, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        
        // log the error
        using (LogContext.PushProperty("ExeptionEventId", exceptionModel.Id))
        using (LogContext.PushProperty("Path", context.Request.Path.Value))
        using (LogContext.PushProperty("Data", data))
        {
            _logger.LogError(exception, exceptionModel.Data["Message"]);
        }
    }
}
