using System.Net;
using System.Text.Json;
using StudentManagementApi.Exceptions;

namespace StudentManagementApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (UserAlreadyExistsException ex)
        {
            logger.LogWarning(
                ex,
                "User registration failed because the user already exists."
            );

            await HandleExceptionAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message
            );
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An unhandled exception occurred."
            );

            await HandleExceptionAsync(
                context,
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred."
            );
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.StatusCode = (int)statusCode;

        context.Response.ContentType =
            "application/json";

        var response = new
        {
            statusCode = (int)statusCode,
            message = message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response)
        );
    }
}   