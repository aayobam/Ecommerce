using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.ApplicationUser.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository<Domain.Entities.ApplicationUser> _userRepository;
    public CreateUserCommandValidator(IUserRepository<Domain.Entities.ApplicationUser> userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.LastName)
            .NotNull().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.PhoneNumber)
            .NotNull().WithMessage("{PropertyName} is required.");

        RuleFor(x => x)
            .MustAsync(PhoneNumberIsUnique).WithMessage("{PropertyName} already exist.");

        RuleFor(x => x.EmailAddress)
            .NotNull().WithMessage("{PropertyName} is requested.")
            .EmailAddress().WithMessage("Invalid email.");

        RuleFor(x => x)
            .MustAsync(EmailAddressNameIsUnique).WithMessage("{PropertyName} already exist.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Matches(x => x.Password).WithMessage("passwords and confirm password do not match.");
    }

    private async Task<bool> EmailAddressNameIsUnique(CreateUserCommand command, CancellationToken token)
    {
        return await _userRepository.IsEmailAddressUniqueAsync(command.EmailAddress);
    }

    private async Task<bool> PhoneNumberIsUnique(CreateUserCommand command, CancellationToken token)
    {
        return await _userRepository.IsPhoneNumberUniqueAsync(command.PhoneNumber);
    }
}
