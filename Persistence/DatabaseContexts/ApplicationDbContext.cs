using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseContexts;

public class ApplicationDbContext : DbContext
{
    protected ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    }
}
