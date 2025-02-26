using Domain.Common;

namespace Domain.Entities;

public class Review : BaseEntity
{
    
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Product Product { get; set; }
    public virtual ApplicationUser Customer { get; set; }
    public string Comments { get; set; }
    public int Rating { get; set; } = 1;
    public float AverageRating { get; set; }
}
