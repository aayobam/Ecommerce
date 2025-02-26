using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum OrderStatus
{
    [Display(Name = "Pending")]
    Pending,

    [Display(Name = "Processed")]
    Processed,

    [Display(Name = "Cancelled")]
    Cancelled
}

public enum DeliveryStatus
{
    [Display(Name = "Pending")]
    Pending,

    [Display(Name = "Assigned To Driver")]
    AssingedToDriver,

    [Display(Name = "In Transitr")]
    InTransit,

    [Display(Name = "Delivered")]
    Delivered,

    [Display(Name = "Failed")]
    Failed
}

public enum PaymentStatus
{
    [Display(Name = "Confirmed")]
    Confirmed,

    [Display(Name = "Unconfirmed")]
    Unconfirmed,

    [Display(Name = "Failed")]
    Failed
}
