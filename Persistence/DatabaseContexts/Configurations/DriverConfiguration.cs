using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DatabaseContexts.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasOne(x => x.Logistic)
            .WithMany(x => x.Drivers)
            .HasForeignKey(x => x.LogisticId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(x => x.Orders)
            .WithOne(o => o.Driver)
            .HasForeignKey(o => o.DriverId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Driver)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);
    }
}
