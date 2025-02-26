namespace Application.Exceptions;

public class EcommerceNotFoundException : CustomException
{

    public EcommerceNotFoundException(string message) : base(message)
    {

    }

    public EcommerceNotFoundException(string message, string code) : base(message, code)
    {

    }
}
