namespace Abstractions.Exceptions;

public abstract class EcommerceException : Exception
{
    protected EcommerceException(string message):base(message)
    {

    }
}
