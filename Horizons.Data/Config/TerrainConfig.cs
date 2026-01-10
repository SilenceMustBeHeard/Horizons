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
              new Terrain  { Id = 1, Name = "Mountain" },
              new Terrain  { Id = 2, Name = "Beach" },
               new Terrain { Id = 3, Name = "Forest" },
                new Terrain { Id = 4, Name = "Plain" },
                new Terrain { Id = 5, Name = "Urban" },
                new Terrain { Id = 6, Name = "Village" },
                new Terrain { Id = 7, Name = "Cave" },
                new Terrain { Id = 8, Name = "Canyon" }
            };

            return seedTerrain;
        }
    }
}
