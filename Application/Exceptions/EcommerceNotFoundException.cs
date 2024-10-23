using Abstractions.Exceptions;

namespace Domain.Exceptions;

public class EcommerceNotFoundException : EcommerceException
{
    public EcommerceNotFoundException(string message) : base(message)
    {
    }
}
