using Domain.Dtos.ApplicationUser;

namespace Application.DTOs.Auth;

public class TokenDataVm
{
    public UserVm User { get; set; }
    public string AccessToken { get; set; }   
}
