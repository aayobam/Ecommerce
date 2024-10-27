using Abstractions.Exceptions;
using System.Net;

namespace Application.Exceptions;

public class EcommerceInternalServerErrorException : CustomException
{
    public EcommerceInternalServerErrorException(string message, string code) : base("an error occured please try again", HttpStatusCode.InternalServerError.ToString())
    {

    }
}
