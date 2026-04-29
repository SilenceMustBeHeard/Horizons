using Horizons.Data.Models;
using Horizons.Data.Models.Base;
using Horizons.Data.Models.Messages;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Terrain> Terrains { get; set; }
 
    public virtual DbSet<Favorite> Favorites { get; set; } = null!;
    public virtual DbSet<ContactMessage> ContactMessages { get; set; } = null!;
    public virtual DbSet<SystemInboxMessage> SystemInboxMessages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}