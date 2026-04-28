using Horizons.Data.Models;
using Horizons.GCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizons.Data.Config;

public class TerrainConfig : IEntityTypeConfiguration<Terrain>
{

    public void Configure(EntityTypeBuilder<Terrain> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(ValidationConstants.TerrainNameMaxLength);


        builder.HasMany(t => t.Destinations)
            .WithOne(d => d.Terrain)
            .HasForeignKey(d => d.TerrainId)
            .OnDelete(DeleteBehavior.Restrict);


      
    }


    
}
