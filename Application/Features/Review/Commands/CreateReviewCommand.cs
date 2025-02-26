using Application.Responses;
using MediatR;

namespace Application.Features.Review.Commands;

public class CreateReviewCommand : IRequest<GenericResponse>
{
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Domain.Entities.Product Product { get; set; }
    public virtual Domain.Entities.ApplicationUser Customer { get; set; }
    public string Comments { get; set; }
    public int Rating { get; set; } = 0;
    public float AverageRating { get; set; } = 0;
}
