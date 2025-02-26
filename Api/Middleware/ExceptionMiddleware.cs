using Api.Models;
using Application.Exceptions;
using System.Net;

namespace Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        dynamic problem;

        switch (exception)
        {
            case EcommerceBadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails()
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(EcommerceBadRequestException),
                    Errors = badRequestException.ValidationErrors 
                };
                break;

            case EcommerceInternalServerErrorException internalServerErrorException:
                statusCode = HttpStatusCode.InternalServerError;
                problem = new CustomProblemDetails()
                {
                    Title = internalServerErrorException.Message,
                    Status = (int)statusCode,
                    Detail = internalServerErrorException.InnerException?.Message,
                    Type = nameof(EcommerceInternalServerErrorException),
                };
                break;

            case EcommerceServiceUnavailableException serviceUnavailableErrorException:
                statusCode = HttpStatusCode.ServiceUnavailable;
                problem = new CustomProblemDetails()
                {
                    Title = serviceUnavailableErrorException.Message,
                    Status = (int)statusCode,
                    Detail = serviceUnavailableErrorException.InnerException?.Message,
                    Type = nameof(EcommerceServiceUnavailableException),
                };
                break;

            case EcommerceUnauthorizedException unauthorizedException:
                statusCode = HttpStatusCode.Unauthorized;
                problem = new CustomProblemDetails()
                {
                    Title = unauthorizedException.Message,
                    Status = (int)statusCode,
                    Detail = unauthorizedException.InnerException?.Message,
                    Type = nameof(EcommerceUnauthorizedException),
                };
                break;

            case EcommerceNotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails()
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Detail = notFoundException.InnerException?.Message,
                    Type = nameof(EcommerceNotFoundException),
                };
                break;

            default:
                problem = new CustomProblemDetails()
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = exception.StackTrace,
                };
                break;
        }
        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync((string)problem);
    }
}
