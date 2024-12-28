using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Pally.Application.Common;
using Pally.Application.Exceptions;
using System;

namespace Pally.Application.Filters
{
    public class ValidationFilter<T> : ActionFilterAttribute, IActionFilter where T : class
    {
        public async override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var validator = filterContext.HttpContext.RequestServices.GetService<IValidator<T>>();
                var objectToValidate = filterContext.ActionArguments["model"] as T;

                if (objectToValidate == null)
                {
                    throw new PallyBadRequestException("The data sent is not correct");
                }

                var validationResult = await validator.ValidateAsync(objectToValidate);

                if (!validationResult.IsValid)
                {
                    var error = validationResult.Errors.ToList();
                    var errorResponse = new GenericResponse
                    {
                        StatusCode = error.ErrorCode,
                        Message = error.ErrorMessage
                    };

                    filterContext.Result = new BadRequestObjectResult(errorResponse);
                    return;
                }
                else
                {
                    base.OnActionExecuting(filterContext);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new GenericResponse
                {
                    StatusCode = "500",
                    Message = ex.Message
                };

                filterContext.Result = new BadRequestObjectResult(errorResponse);
            }
        }
    }
}
