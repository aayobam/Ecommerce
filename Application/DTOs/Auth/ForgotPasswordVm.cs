using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth;

public class ForgotPasswordVm
{
    [EmailAddress]
    public string Email { get; set; }
}
