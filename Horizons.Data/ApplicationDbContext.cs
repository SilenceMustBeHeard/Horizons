using Horizons.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser> // Use AppUser, not IdentityUser
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Terrain> Terrains { get; set; }
    public DbSet<UserDestination> UsersDestinations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}