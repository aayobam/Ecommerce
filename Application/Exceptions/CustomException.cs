namespace Application.Exceptions;

public abstract class CustomException : Exception
{
    public string Code { get; set; }

    protected CustomException(string message) : base(message)
    {

    }

    protected CustomException(string message, string code) : base(message)
    {
        Code = code;
    }
}
