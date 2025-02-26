using Application.DTOs.Role;
using Domain.Dtos.ApplicationUser;
namespace Application.DTOs.ApplicationUser;

public class UserRolesVm
{
    public UserVm User { get; set; }
    public List<RoleVm> RolesVms { get; set; }
}
