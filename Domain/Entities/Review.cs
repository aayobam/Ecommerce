using Domain.Common;

namespace Domain.Entities;

public class Review : BaseEntity
{
    public string Comments { get; set; }
    public int Rating { get; set; } = 0;
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Products { get; set; }
}
