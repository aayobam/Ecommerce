using Abstractions.Exceptions;
using FluentValidation.Results;

namespace Application.Exceptions;

public class EcommerceBadRequestException : CustomException
{
    public List<string> ValidationErrors { get; set; }

    public EcommerceBadRequestException(string message, string code) : base(message, code)
    {

    }

    public EcommerceBadRequestException(string message, ValidationResult validationResult, string code) : base(message, code)
    {
        ValidationErrors = new();

        foreach (var error in validationResult.Errors)
        {
            ValidationErrors.Add(error.ErrorMessage);
        }
    }
}
