using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
{
    public UserRepository(EcommerceDbContext context) : base(context)
    {
    }
}
