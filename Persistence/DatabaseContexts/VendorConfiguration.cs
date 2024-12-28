using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseContexts;

public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.HasMany(x => x.Products)
            .WithOne()
            .HasForeignKey(x => x.VendorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Users)
            .WithOne()
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Orders)
            .WithOne()
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
