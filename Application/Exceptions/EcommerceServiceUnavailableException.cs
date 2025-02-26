using System.Net;

namespace Application.Exceptions;

public class EcommerceServiceUnavailableException : CustomException
{
    public EcommerceServiceUnavailableException(string message, string code) : base("service unavailable", code)
    {

    }
}
