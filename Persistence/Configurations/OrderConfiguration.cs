using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Vendor)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.VendorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Driver)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.DriverId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
