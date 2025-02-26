using Domain.Common;
using Domain.Dtos.ApplicationUser;
using Domain.Dtos.Product;

namespace Application.DTOs.Review;

public class ReviewVm : BaseEntity
{
    public UserVm User { get; set; }
    public ProductVm Product { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public decimal AverageRating { get; set; }
}
