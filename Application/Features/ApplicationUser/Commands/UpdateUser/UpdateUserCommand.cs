using Domain.Dtos.ApplicationUser;
using MediatR;

namespace Application.Features.ApplicationUser.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<UserVm>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
}
