using Horizons.Data.Models;
using Horizons.GCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizons.Data.Config
{
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


           builder.HasData(GenerateSeedTerrain());
        }


        private List<Terrain> GenerateSeedTerrain()
        {
            var seedTerrain = new List<Terrain>()
            { 
              new Terrain  { Id = Guid.NewGuid(), Name = "Mountain" },
              new Terrain  { Id = Guid.NewGuid(), Name = "Beach" },
               new Terrain { Id = Guid.NewGuid(), Name = "Forest" },
                new Terrain { Id = Guid.NewGuid(), Name = "Plain" },
                new Terrain { Id = Guid.NewGuid(), Name = "Urban" },
                new Terrain { Id = Guid.NewGuid(), Name = "Village" },
                new Terrain { Id = Guid.NewGuid(), Name = "Cave" },
                new Terrain { Id = Guid.NewGuid(), Name = "Canyon" }
            };

            return seedTerrain;
        }
    }
}
