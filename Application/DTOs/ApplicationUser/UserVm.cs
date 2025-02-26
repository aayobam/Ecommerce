using Application.DTOs.Role;
using Domain.Common;

namespace Domain.Dtos.ApplicationUser;

public class UserVm : BaseEntity
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}
