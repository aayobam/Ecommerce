using Abstractions.Exceptions;
using FluentValidation.Results;

namespace Application.Exceptions;

public class EcommerceBadRequestException : CustomException
{
    public IDictionary<string, string[]> ValidationErrors { get; set; }

    public EcommerceBadRequestException(string message, string code) : base(message, code)
    {

    }

    public EcommerceBadRequestException(ValidationResult validationResult, string code) : base(code)
    {
        ValidationErrors = validationResult.ToDictionary();

        //foreach (var error in validationResult.Errors)
        //{
        //    ValidationErrors.Add(error.ErrorMessage);
        //}
    }
}
