using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.ApplicationUser.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserRepository<Domain.Entities.ApplicationUser> _userRepository;

    public UpdateUserCommandValidator(IUserRepository<Domain.Entities.ApplicationUser> userRepository)
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

    }

    private async Task<bool> EmailAddressNameIsUnique(UpdateUserCommand command, CancellationToken token)
    {
        return await _userRepository.IsEmailAddressUniqueAsync(command.EmailAddress);
    }

    private async Task<bool> PhoneNumberIsUnique(UpdateUserCommand command, CancellationToken token)
    {
        return await _userRepository.IsPhoneNumberUniqueAsync(command.PhoneNumber);
    }
}
