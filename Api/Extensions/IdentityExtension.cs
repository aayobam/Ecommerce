using Application.DTOs.ApplicationUser;
using System.Security.Claims;

namespace Api.Extensions;

public static class IdentityExtension
{
    public static UserDetailsVm GetSessionDetails(this ClaimsPrincipal principal)
    {
        try
        {
            var identity = (ClaimsIdentity)principal.Identity!;
            var getData = new UserDetailsVm
            {
                EmailAddress = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                ApplicationUserId = Guid.Parse(identity.Claims.FirstOrDefault(c => c.Type == "id")!.Value).ToString(),
                Role = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
            };
            return getData;
        }
        catch (Exception)
        {
            return null!;
        }
    }
}
