using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class ReviewService : GenericRepository<Review>, IReviewRepository
{
    public ReviewService(EcommerceDbContext context) : base(context)
    {

    }
}