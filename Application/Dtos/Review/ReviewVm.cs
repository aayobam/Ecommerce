using Domain.Common;
using Domain.Dtos.ApplicationUser;
using Domain.Dtos.Product;

namespace Application.DTOs.Review;

public class ReviewVm : BaseEntity
{
    public Guid UserId { get; set; }
    public UserVm User { get; set; }
    public Guid ProductId { get; set; }
    public ProductVm Product { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
}
