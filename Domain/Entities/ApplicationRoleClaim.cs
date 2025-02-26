using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual ApplicationRole Role { get; set; }
    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset DateModified { get; set; } = DateTimeOffset.UtcNow;
}
