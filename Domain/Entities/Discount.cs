using Domain.Common;

namespace Domain.Entities;

public class Discount : BaseEntity
{
    public string Code { get; set; }
    public decimal Percentage { get; set; } = 0;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}
