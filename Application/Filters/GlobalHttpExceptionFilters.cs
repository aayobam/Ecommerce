using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Pally.Application.Common;
using Pally.Application.Exceptions;
using Pally.Application.Extensions;
using System.Net;

namespace Pally.Application.Filters
{
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
            GenericResponse response;

            switch (context.Exception)
            {
                case PallyNotFoundException e:
                    code = HttpStatusCode.NotFound;
                    response = e.ToErrorResponse();
                    break;
                case PallyValidationException e:
                    code = HttpStatusCode.NotAcceptable;
                    response = e.ToErrorResponse();
                    break;
                case PallyServiceUnavailableException e:
                    code = HttpStatusCode.BadRequest;
                    response = e.ToErrorResponse();
                    break;
                case PallyUnAuthorizedException e:
                    code = HttpStatusCode.Unauthorized;
                    response = e.ToErrorResponse();
                    break;
                case PallyForbiddenException e:
                    code = HttpStatusCode.Forbidden;
                    response = e.ToErrorResponse();
                    break;
                case PallyBadRequestException e:
                    code = HttpStatusCode.BadRequest;
                    response = e.ToErrorResponse();
                    break;
                case PallySystemErrorException e:
                    code = HttpStatusCode.InternalServerError;
                    response = e.ToErrorResponse();
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    response = context.Exception.ToErrorResponse();
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
}
