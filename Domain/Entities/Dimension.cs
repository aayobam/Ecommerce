using Domain.Common;

namespace Domain.Entities;

public class Dimension : BaseEntity
{
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
    public string Height { get; set; }
    public string Width { get; set; }
    public string breadth { get; set; }
    public string Weight { get; set; }
}
