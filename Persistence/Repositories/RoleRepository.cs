using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class RoleRepository : GenericRepository<ApplicationUser>, IRoleRepository
{
    public RoleRepository(EcommerceDbContext context) : base(context)
    {
    }
}

