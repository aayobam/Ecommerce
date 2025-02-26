namespace Application.DTOs.Review;

public class CreateReviewVm
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; } = 0;
}
