using Horizons.Data.Models;
using Horizons.GCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizons.Data.Config;

public class DestinationConfig : IEntityTypeConfiguration<Destination>
{
    public void Configure(EntityTypeBuilder<Destination> builder)
    {
        builder.HasKey(d => d.Id);

        // Relationships only
        builder.HasOne(d => d.Publisher)
               .WithMany()
               .HasForeignKey(d => d.PublisherId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Terrain)
               .WithMany(t => t.Destinations)
               .HasForeignKey(d => d.TerrainId)
               .OnDelete(DeleteBehavior.Restrict);

        // One-to-many with Favorites
        builder.HasMany(d => d.Favorites)
               .WithOne(f => f.Destination)
               .HasForeignKey(f => f.DestinationId)
               .OnDelete(DeleteBehavior.Restrict);

        // Soft delete filter
        builder.HasQueryFilter(d => !d.IsDeleted);
    }
}