using Horizons.Data.Models;
using Horizons.GCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Horizons.Data.Config
{
    public class DestinationConfig : IEntityTypeConfiguration<Destination>
    {


        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                     .IsRequired()
                   .HasMaxLength(ValidationConstants.DestinationNameMaxLength);

            builder.Property(d => d.Description)
                        .IsRequired()
                        .HasMaxLength(ValidationConstants.DestinationDescriptionMaxLength);

            builder.Property(d => d.ImageUrl)
                        .IsRequired(false);

            builder.Property(d => d.PublisherId)
                        .IsRequired();
            builder.Property(d => d.IsDeleted)
                         .HasDefaultValue(false);

            builder.HasQueryFilter(d => !d.IsDeleted);

            builder.HasOne(d => d.Publisher)
                        .WithMany()
                        .HasForeignKey(d => d.PublisherId)
                        .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Terrain)
                        .WithMany(t => t.Destinations)
                        .HasForeignKey(d => d.TerrainId)
                        .OnDelete(DeleteBehavior.Restrict);




            builder.HasData(GenerateSeedDestinations());
        }


        private List<Destination> GenerateSeedDestinations()
        {
            var seedDestinations = new List<Destination>()
            {
                new Destination
                {
                    Id = Guid.NewGuid(),
                    Name = "Sunny Beach",
                    Description = "A beautiful sunny beach with golden sands and clear waters.",
                    ImageUrl = "https://example.com/images/sunny_beach.jpg",
                    PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
                    CreatedAt = new DateTime(2023, 2, 12),
                    TerrainId = Guid.NewGuid()
                },
                new Destination
                {
                    Id =Guid.NewGuid(),
                    Name = "Misty Mountains",
                    Description = "A range of mist-covered mountains perfect for hiking and adventure.",
                    ImageUrl = "https://example.com/images/misty_mountains.jpg",
                    PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
                    CreatedAt = new DateTime(2025, 1, 1),
                    TerrainId = Guid.NewGuid()
                },
                new Destination
                {
                    Id =Guid.NewGuid(),
                    Name = "Rila Monastery",
                    Description = "A stunning historical landmark nestled in the Rila Mountains.",
                    ImageUrl = "https://img.etimg.com/thumb/msid-112831459,width-640,height-480,imgsize-2180890,resizemode-4/rila-monastery-bulgaria.jpg",
                    PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
                    CreatedAt = new DateTime(2022, 12, 1),
                    TerrainId = Guid.NewGuid(),
                    IsDeleted = false
                },
                 new Destination
                {
                    Id = Guid.NewGuid(),
                    Name = "Durankulak Beach",
                    Description = "The sand at Durankulak Beach showcases a pristine golden color, creating a beautiful contrast against the azure waters of the Black Sea.",
                    ImageUrl = "https://travelplanner.ro/blog/wp-content/uploads/2023/01/durankulak-beach-1-850x550.jpg.webp",
                    PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
                    CreatedAt = new DateTime(2024, 1, 1),
                    TerrainId = Guid.NewGuid(),
                    IsDeleted = false
                },
                new Destination
                {
                    Id =Guid.NewGuid(),
                    Name = "Devil's Throat Cave",
                    Description = "A mysterious cave located in the Rhodope Mountains.",
                    ImageUrl = "https://detskotobnr.binar.bg/wp-content/uploads/2017/11/Diavolsko_garlo_17.jpg",
                    PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
                    CreatedAt = new DateTime(2025, 10, 1),
                    TerrainId = Guid.NewGuid(),
                    IsDeleted = false
                }


            };
            return seedDestinations;
        }
    }
}



