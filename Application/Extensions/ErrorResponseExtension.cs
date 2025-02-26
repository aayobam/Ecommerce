using Application.Exceptions;
using Application.Responses;

namespace Application.Extensions;

public static class ErrorResponseExtension
{
    public static ErrorResponse ToErrorResponse(this EcommerceNotFoundException e)
    {
        return new ErrorResponse()
        {
            Success = false,
            StatusCode = e.Code,
            Message = e.Message,
        };
    }

    public static ErrorResponse ToErrorResponse(this EcommerceBadRequestException e)
    {
        return new ErrorResponse()
        {
            Success = false,
            StatusCode = e.Code,
            Message = e.Message,
        };
    }

    public static ErrorResponse ToErrorResponse(this EcommerceServiceUnavailableException e)
    {
        return new ErrorResponse()
        {
            Success = false,
            StatusCode = e.Code,
            Message = e.Message,
        };
    }

    public static ErrorResponse ToErrorResponse(this EcommerceInternalServerErrorException e)
    {
        return new ErrorResponse()
        {
            Success = false,
            StatusCode = e.Code,
            Message = e.Message,
        };
    }

    public static ErrorResponse ToErrorResponse(this EcommerceUnauthorizedException e)
    {
        return new ErrorResponse()
        {
            Success = false,
            StatusCode = e.Code,
            Message = e.Message,
        };
    }

    public static ErrorResponse ToErrorResponse(this EcommerceForbiddenException e)
    {
        return new ErrorResponse()
        {
            Success = false,
            StatusCode = e.Code,
            Message = e.Message,
        };
    }
}
