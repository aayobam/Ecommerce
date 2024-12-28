using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseContexts.Configurations;

public class LogisticConfiguration : IEntityTypeConfiguration<Logistic>
{
    public void Configure(EntityTypeBuilder<Logistic> builder)
    {
        builder.HasMany(l => l.Drivers)
            .WithOne(d => d.Logistic)
            .HasForeignKey(d => d.LogisticId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.Users)
            .WithOne(u => u.Logistic)
            .HasForeignKey(u => u.LogisticId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
