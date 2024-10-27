using Domain.Common;

namespace Domain.Entities;

public class Size : BaseEntity
{
    public string Height { get; set; }
    public string Width { get; set; }
    public string breadth { get; set; }
    public string Weight { get; set; }
}
