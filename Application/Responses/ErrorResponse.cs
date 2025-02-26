namespace Application.Responses;

public class ErrorResponse
{
    public bool Success { get; set;}
    public string StatusCode { get; set; }
    public string Message { get; set; }
}
