namespace Application.Exceptions;

public class EcommerceInternalServerErrorException : CustomException
{
    public EcommerceInternalServerErrorException(string message, string code) : base("an internal service error occured, please try again", code)
    {

    }
}
