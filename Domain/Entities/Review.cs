using Domain.Common;

namespace Domain.Entities;

public class Review : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
    public string Comments { get; set; }
    public int Rating { get; set; } = 0;
    public float AverageRating { get; set; } = 0;
}
