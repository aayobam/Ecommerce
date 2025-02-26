using Domain.Common;

namespace Domain.Entities;

public class VerificationOtp : BaseEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public DateTimeOffset Expiry { get; set; }
}
