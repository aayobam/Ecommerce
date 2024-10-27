namespace Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset DateModified { get; set; } = DateTimeOffset.Now;
}
