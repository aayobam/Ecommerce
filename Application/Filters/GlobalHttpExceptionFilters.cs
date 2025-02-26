using Application.Exceptions;
using Application.Extensions;
using Application.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace Application.Filters;

public class HttpGlobalExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<HttpGlobalExceptionFilter> _logger;

    public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
    {
        _env = env;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

        _logger.LogInformation($"Environment: {_env.EnvironmentName}");

        HttpStatusCode code;

        ErrorResponse response;

        switch (context.Exception)
        {
            case EcommerceNotFoundException e:
                code = HttpStatusCode.NotFound;
                response = e.ToErrorResponse();
                break;
            case EcommerceServiceUnavailableException e:
                code = HttpStatusCode.BadRequest;
                response = e.ToErrorResponse();
                break;
            case EcommerceUnauthorizedException e:
                code = HttpStatusCode.Unauthorized;
                response = e.ToErrorResponse();
                break;
            case EcommerceForbiddenException e:
                code = HttpStatusCode.Forbidden;
                response = e.ToErrorResponse();
                break;
            case EcommerceBadRequestException e:
                code = HttpStatusCode.BadRequest;
                response = e.ToErrorResponse();
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                response = new ErrorResponse
                {
                    StatusCode = code.ToString(),
                    Message = context.Exception.Message,
                };
                break;
        }

        var contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        var serializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.Indented
        };
        var result = JsonConvert.SerializeObject(response, serializerSettings);
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)code;
        context.HttpContext.Response.WriteAsync(result);
        context.ExceptionHandled = true;
    }
}
