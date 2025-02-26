namespace Domain.Dtos.ApplicationUser;

public class CreateUserVm
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<string> RoleNames{ get; set; }
}
