using Domain.Common;

namespace Domain.Entities;

public class OtpVerification : BaseEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string Otp { get; set; }
    public DateTimeOffset Expiry { get; set; }
    public bool Expired { get; set; }
    public string Reason { get; set; }
}
