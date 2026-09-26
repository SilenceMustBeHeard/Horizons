using Horizons.Data;
using Horizons.Data.Models;
using Horizons.Data.Models.Base;
using Horizons.Data.Repositories.Implementations.Base;
using Horizons.Data.Repositories.Interfaces.Base;
using Horizons.Data.Seeding;
using Horizons.Services.Core.Implementations;
using Horizons.Services.Core.Implementations.Account;
using Horizons.Services.Core.Interfaces;
using Horizons.Services.Core.Interfaces.Account;
using Horizons.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using SendGrid;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add HttpClient factory
builder.Services.AddHttpClient();

// Add Identity
builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = true;

    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 10;
    options.Password.RequiredUniqueChars = 4;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", p => p.RequireRole("Admin"));
    options.AddPolicy("ManagerPolicy", p => p.RequireRole("Manager"));
});

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "Horizons.Session";
});

builder.Services.AddHttpContextAccessor();

// Repositories & Services
builder.Services.RegisterRepositories(typeof(ITerrainRepository).Assembly);
builder.Services.RegisterServices(typeof(IDestinationService).Assembly);

builder.Services.AddScoped<ITerrainService, TerrainService>();

// SendGrid
builder.Services.AddSingleton(sp =>
{
    var apiKey = builder.Configuration["SendGrid:ApiKey"];
    if (string.IsNullOrEmpty(apiKey))
    {
        apiKey = builder.Configuration.GetValue<string>("SendGrid:ApiKey");
    }

    if (string.IsNullOrEmpty(apiKey))
    {
        throw new InvalidOperationException("SendGrid API Key is missing. Add it via: dotnet user-secrets set \"SendGrid:ApiKey\" \"YOUR_KEY\"");
    }

    return new SendGridClient(apiKey);
});

builder.Services.AddScoped<IEmailService, EmailService>();

// MVC
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configure error handling
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();


// MIDDLEWARE PIPELINE


// Seed Roles, Users and Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var configuration = services.GetRequiredService<IConfiguration>();

    await context.Database.MigrateAsync();

    await IdentitySeeder.SeedRolesAsync(roleManager);
    await IdentitySeeder.SeedAdminAsync(userManager, configuration);
    await IdentitySeeder.SeedManagerAsync(userManager, configuration);

    if (!await context.Destinations.AnyAsync())
    {
        try
        {
            await DbSeeder.SeedAllAsync(context);
            Console.WriteLine("✅ Database seeding completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Seeding failed: {ex.Message}");
            throw;
        }
    }
    else
    {
        Console.WriteLine("📦 Destinations already exist. Skipping data seeding.");
    }
}

// Static files with .glb support
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".glb"] = "model/gltf-binary";

// Error handling middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/500");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});


app.UseRouting();                                    

app.UseStatusCodePagesWithReExecute("/Error/{0}");  

app.UseAuthentication();                            
app.UseAuthorization();

app.UseSession();                                    


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecks("/health");

await app.RunAsync();