using Domain.Common;

namespace Domain.Entities;

public class Variation : BaseEntity
{
    public string Name { get; set; }
    public virtual Product Product { get; set; }
}
