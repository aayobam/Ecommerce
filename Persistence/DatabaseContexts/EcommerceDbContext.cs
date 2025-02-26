using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.DatabaseContexts;

public class EcommerceDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(nameof(EcommerceDbContext)).AddInterceptors(new SlowQueryInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcommerceDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTimeOffset.UtcNow;
            } 
            entry.Entity.DateModified = DateTimeOffset.UtcNow;
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationRole> Roles { get; set; }
    public DbSet<ApplicationUserRole> UserRoles { get; set; }
    public DbSet<ApplicationRoleClaim> RoleClaims {  get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Logistic> Logistics { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<WishList> WishLists { get; set; }
    public DbSet<Vendor> Reviews { get; set; }
    public DbSet<AuditTrail> AuditTrails { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<VerificationOtp> VerificationOpts { get; set; }
}
