using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class ReviewRepository : GenericRepository<ApplicationUser>, IReviewRepository
{
    public ReviewRepository(EcommerceDbContext context) : base(context)
    {
    }
}