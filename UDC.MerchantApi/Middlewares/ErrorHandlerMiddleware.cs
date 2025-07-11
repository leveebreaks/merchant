﻿using System.Net;
using System.Text.Json;

namespace UDC.MerchantApi.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        // custom error catches go here
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await GenerateExceptionResponse(context, "An error has occurred.", HttpStatusCode.InternalServerError);
        }
    }

    private async Task GenerateExceptionResponse(HttpContext context, string message, HttpStatusCode statusCode)
    {
        var errorResponse = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = message
        };
        var json = JsonSerializer.Serialize(errorResponse);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorResponse.StatusCode;
        await context.Response.WriteAsync(json);
    }
}

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
}