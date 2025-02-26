using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class VendorService : GenericRepository<Vendor>, IVendorRepository
{
    public VendorService(EcommerceDbContext context) : base(context)
    {

    }
}
