using Application.Exceptions;
using Application.Responses;
using Domain.Exceptions;

namespace Api.Extensions;

public static class ErrorResponseExtension
{
    public static GenericResponse ToErrorResponse(this EcommerceNotFoundException e)
    {
        return new GenericResponse()
        {
            Success = false,
            Status = e.Code,
            Message = e.Message
        };
    }

    public static GenericResponse ToErrorResponse(this EcommerceBadRequestException e)
    {
        return new GenericResponse()
        {
            Success = false,
            Status = e.Code,
            Message = e.Message

        };
    }

    public static GenericResponse ToErrorResponse(this EcommerceServiceUnavailableException e)
    {
        return new GenericResponse()
        {
            Success = false,
            Status = e.Code,
            Message = e.Message
        };
    }

    public static GenericResponse ToErrorResponse(this EcommerceInternalServerErrorException e)
    {
        return new GenericResponse()
        {
            Success = false,
            Status = e.Code,
            Message = e.Message
        };
    }

    public static GenericResponse ToErrorResponse(this EcommerceUnauthorizedException e)
    {
        return new GenericResponse()
        {
            Success = false,
            Status = e.Code,
            Message = e.Message
        };
    }
}
