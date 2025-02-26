using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth;

public class UserLoginVm
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}
