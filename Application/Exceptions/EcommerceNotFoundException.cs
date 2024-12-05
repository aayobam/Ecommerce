using Abstractions.Exceptions;
using System.Net;

namespace Domain.Exceptions;

public class EcommerceNotFoundException : CustomException
{
 
    public EcommerceNotFoundException(string message) : base(message)
    {
        
    }

    public EcommerceNotFoundException(string objectName, object id, string code) : base($"{objectName} : {id} not found", code)
    {
        Code = code;
    }
}
