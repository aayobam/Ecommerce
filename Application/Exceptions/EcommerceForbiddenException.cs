namespace Application.Exceptions;

public class EcommerceForbiddenException : CustomException
{
    public EcommerceForbiddenException(string message) : base(message)
    {

    }

    public EcommerceForbiddenException(string message, string code) : base($"UnauthorizedAccess: You are not allowed to access this resource.", "Access Denied")
    {

    }
}
