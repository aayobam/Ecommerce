using Abstractions.Exceptions;
using System.Net;

namespace Domain.Exceptions;

public class EcommerceNotFoundException : CustomException
{
 
    public EcommerceNotFoundException(string message) : base(message)
    {
        
    }

    public EcommerceNotFoundException(string name, object key, string code) : base($"{name} : {key} not found", code)
    {
        Code = code;
    }
}
