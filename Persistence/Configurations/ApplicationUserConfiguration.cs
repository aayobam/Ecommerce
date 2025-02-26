using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Configure MiddleName as optional
        builder.Property(x => x.MiddleName)
            .IsRequired(false);

        // Configure UserRoles relationship
        builder.HasMany(x => x.UserRoles)
            .WithOne(e => e.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);

        // Configure Addresses relationship
        builder.HasMany(x => x.Addresses)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);

        // Configure Vendor relationship
        builder.HasOne(x => x.Vendor)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.VendorId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        // Configure Orders relationship
        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(true);

        // Configure Logistic relationship
        builder.HasOne(x => x.Logistic)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.LogisticId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}