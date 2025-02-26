using System.Net;

namespace Application.Exceptions;

public class EcommerceUnauthorizedException : CustomException
{
    public EcommerceUnauthorizedException(string message, string code) : base("You are not authorized to perform this operation.", code)
    {

    }
}
