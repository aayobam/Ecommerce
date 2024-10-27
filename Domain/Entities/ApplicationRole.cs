using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationRole : IdentityRole
{
    public override string Name { get; set; }
    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset DateModified { get; set; } = DateTimeOffset.Now;
}
