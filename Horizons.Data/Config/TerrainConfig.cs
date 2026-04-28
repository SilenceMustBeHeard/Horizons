using Horizons.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizons.Data.Config;

public class TerrainConfig : IEntityTypeConfiguration<Terrain>
{
    public void Configure(EntityTypeBuilder<Terrain> builder)
    {
        builder.HasKey(t => t.Id);

        // Relationships only
        builder.HasMany(t => t.Destinations)
               .WithOne(d => d.Terrain)
               .HasForeignKey(d => d.TerrainId)
               .OnDelete(DeleteBehavior.Restrict);

        // Soft delete filter
        builder.HasQueryFilter(t => !t.IsDeleted);
    }
}