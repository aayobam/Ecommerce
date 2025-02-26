using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth;

public class SetPasswordVm
{
    [EmailAddress]
    public string Email { get; set; }
    public string Code { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
