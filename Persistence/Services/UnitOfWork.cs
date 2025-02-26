using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class UnitOfWork : IDisposable
{
    private readonly EcommerceDbContext _context;

    public UnitOfWork(EcommerceDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
