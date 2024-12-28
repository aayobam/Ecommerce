namespace Domain.Enums;

public enum Actions
{
    Add,
    Change,
    Delete,
}

public enum OrderStatus
{
    Pending,
    Processed,
    Shipped,
    Delivered,
    Cancelled
}

public enum DeliveryStatus
{
    Pending,
    InTransit,
    Delivered,
    Failed
}
