using System.Net;
using System.Text.Json;
using TaskManagement.Application.Exceptions;

namespace TaskManagement.API.Middlewares;

public class GlobalExceptionMiddleware (RequestDelegate _next, ILogger<GlobalExceptionMiddleware> _logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            await HandleAppException(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await HandleException(
                context,
                HttpStatusCode.InternalServerError,
                "INTERNAL_ERROR",
                "An unexpected error occurred.");
        }
    }

    private async Task HandleAppException(
        HttpContext context,
        AppException ex)
    {
        var status = ex switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            ValidationException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.BadRequest
        };

        _logger.LogWarning(ex, ex.Message);

        await HandleException(context, status, ex.ErrorCode, ex.Message);
    }

    private static async Task HandleException(
        HttpContext context,
        HttpStatusCode status,
        string errorCode,
        string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        var response = new
        {
            success = false,
            errorCode,
            message,
            statusCode = (int)status
        };

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}