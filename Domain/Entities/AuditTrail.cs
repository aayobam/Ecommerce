using Domain.Common;
using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class AuditTrail : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Actions Action { get; set; }
    public string Reason { get; set; }
}
