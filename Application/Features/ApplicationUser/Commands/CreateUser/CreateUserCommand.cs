using Application.Responses;
using MediatR;

namespace Application.Features.ApplicationUser.Commands.CreateUser;

public class CreateUserCommand : IRequest<GenericResponse>
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public List<Guid> RoleIds { get; set; }
}
