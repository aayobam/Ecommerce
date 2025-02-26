using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class LogisticConfiguration : IEntityTypeConfiguration<Logistic>
{
    public void Configure(EntityTypeBuilder<Logistic> builder)
    {
        builder.HasMany(l => l.Users)
            .WithOne(u => u.Logistic)
            .HasForeignKey(u => u.LogisticId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Drivers)
            .WithOne(x => x.Logistic)
            .HasForeignKey(x => x.LogisticId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
