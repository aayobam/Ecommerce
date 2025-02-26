namespace Application.DTOs.Role;

public class AddUserToRolesVm
{
    public Guid UserId { get; set; }
    public ICollection<Guid> RoleIds { get; set; }
}