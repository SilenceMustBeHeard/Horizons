namespace Horizons.Data
{
    using Horizons.Data.Config;
    using Horizons.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Terrain> Terrains { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<UserDestination> UsersDestinations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



        }

        


    }
}
