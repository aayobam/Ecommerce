namespace Application.DTOs.ApplicationUser;

public class SetPasswordVm
{
    public string Otp { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
