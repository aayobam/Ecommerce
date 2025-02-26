using Domain.Common;

namespace Domain.Entities;

public class Image : BaseEntity
{
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
    public string Name { get; set; }
    public Uri Url { get; set; }
}
