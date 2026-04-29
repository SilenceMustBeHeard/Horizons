using Horizons.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizons.Data.Config.Interactions;

public class FavoriteConfig : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        // Composite unique key - one user can favorite a destination only once
        builder.HasIndex(f => new { f.UserId, f.DestinationId })
            .IsUnique();

        // Relationships only - no HasData
        builder.HasOne(f => f.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.Destination)
            .WithMany(d => d.Favorites)  
            .HasForeignKey(f => f.DestinationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}