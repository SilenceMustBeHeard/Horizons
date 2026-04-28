using Horizons.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizons.Data.Config
{
    public class UserDestinationConfig : IEntityTypeConfiguration<UserDestination>
    {
        public void Configure(EntityTypeBuilder<UserDestination> builder)
        {
            builder.HasKey(ud => new { ud.UserId, ud.DestinationId });

            builder.HasQueryFilter(ud => !ud.Destination.IsDeleted);

            builder.HasOne(ud => ud.User)
                   .WithMany(u => u.UsersDestinations)
                   .HasForeignKey(ud => ud.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ud => ud.Destination)
                   .WithMany(d => d.UsersDestinations)
                   .HasForeignKey(ud => ud.DestinationId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}