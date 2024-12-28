using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    private readonly EcommerceDbContext _context;
    public ReviewRepository(EcommerceDbContext context) : base(context)
    {
        _context = context;
    }
}