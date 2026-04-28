using Horizons.Data;
using Horizons.Data.Models;
using Horizons.Data.Models.Base;
using Horizons.Data.Seeding;
using Horizons.Services.Core.Implementations;
using Horizons.Services.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Register Identity with AppUser
        builder.Services.AddDefaultIdentity<AppUser>(options =>
        {
            // Development ease options
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole>() // Add roles support
        .AddEntityFrameworkStores<AppDbContext>();

        // Register services
        builder.Services.AddScoped<IDestinationService, DestinationService>();
        builder.Services.AddScoped<ITerrainService, TerrainService>();

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        // Seed data
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Apply pending migrations
            context.Database.Migrate();

            // Seed identity data
            IdentitySeeder.SeedRolesAsync(roleManager).GetAwaiter().GetResult();
            IdentitySeeder.SeedAdminAsync(userManager).GetAwaiter().GetResult();
            IdentitySeeder.SeedManagerAsync(userManager).GetAwaiter().GetResult();

            DbSeeder.SeedAllAsync(context).GetAwaiter().GetResult();
        }

        app.Run();
    }
}