using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DatabaseContexts.Configurations;

internal class WishListConfiguration : IEntityTypeConfiguration<WishList>
{
    public void Configure(EntityTypeBuilder<WishList> builder)
    {
        builder.HasOne(x => x.User)
            .WithMany(x => x.WishLists)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.WishList)
            .HasForeignKey(x=>x.WishListId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}
