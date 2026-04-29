using Horizons.Data;
using Horizons.Data.Models;
using Horizons.Data.Models.Base;
using Horizons.Services.Core.Admin.Interfaces;
using Horizons.Web.ViewModels.Destination;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Services.Core.Admin.Implementations;

public class DestinationServiceManagement : IDestinationServiceManagement
{
    private readonly AppDbContext context;
    private readonly UserManager<AppUser> userManager;

    public DestinationServiceManagement(AppDbContext context, UserManager<AppUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    public async Task<bool> AddDestinationAsync(DestinationAddInputModel model, string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return false;

        var userExists = await userManager.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
            return false;

        var destination = new Destination
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Description = model.Description,
            TerrainId = model.TerrainId,
            ImageUrl = model.ImageUrl,
            CreatedAt = DateTime.UtcNow,
            PublisherId = userId,
            Country = model.Country,
            Continent = model.Continent,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            TravelDistance = model.TravelDistance,
            IsDeleted = false
        };

        await context.Destinations.AddAsync(destination);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsForAdminAsync()
    {
        return await context.Destinations
            .Include(d => d.Terrain)
            .Include(d => d.UsersDestinations)
            .Include(d => d.Publisher)
            .AsNoTracking()
            .Select(d => new DestinationIndexViewModel
            {
                Id = d.Id,
                Name = d.Name,
                ImageUrl = d.ImageUrl,
                TerrainName = d.Terrain.Name,
                FavouriteCount = d.UsersDestinations.Count,
                IsUserPublisher = true,
                IsUserFavourite = false
            })
            .ToListAsync();
    }

    public async Task<DestinationDetailsViewModel?> GetDestinationDetailsForAdminAsync(Guid id)
    {
        var destination = await context.Destinations
            .Include(d => d.Terrain)
            .Include(d => d.UsersDestinations)
            .Include(d => d.Publisher)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        if (destination == null)
            return null;

        return new DestinationDetailsViewModel
        {
            Id = destination.Id,
            Name = destination.Name,
            Description = destination.Description,
            ImageUrl = destination.ImageUrl,
            TerrainName = destination.Terrain.Name,
            PublishedOn = destination.CreatedAt.ToString("dd.MM.yyyy"),
            PublisherName = destination.Publisher?.UserName ?? "Unknown",
            IsUserPublisher = true,
            IsUserFavourite = false,
            FavoriteCount = destination.UsersDestinations.Count,
            Latitude = destination.Latitude,
            Longitude = destination.Longitude,
            Country = destination.Country,
            Continent = destination.Continent
        };
    }

    public async Task<DestinationEditInputModel?> GetDestinationForEditAsync(string? userId, Guid id)
    {
        if (string.IsNullOrEmpty(userId))
            return null;

        return await context.Destinations
            .AsNoTracking()
            .Where(d => d.Id == id && d.PublisherId == userId)
            .Select(d => new DestinationEditInputModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                ImageUrl = d.ImageUrl,
                TerrainId = d.TerrainId,
                PublishedOn = d.CreatedAt,
                Country = d.Country ?? string.Empty,
                Continent = d.Continent ?? string.Empty,
                Latitude = d.Latitude ?? 0,
                Longitude = d.Longitude ?? 0,
                TravelDistance = d.TravelDistance
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> EditDestinationAsync(DestinationEditInputModel model, string userId)
    {
        var destination = await context.Destinations
            .FirstOrDefaultAsync(d => d.Id == model.Id && d.PublisherId == userId);

        if (destination == null)
            return false;

        destination.Name = model.Name;
        destination.Description = model.Description;
        destination.ImageUrl = model.ImageUrl;
        destination.TerrainId = model.TerrainId;
        destination.UpdatedAt = DateTime.UtcNow;
        destination.Country = model.Country;
        destination.Continent = model.Continent;
        destination.Latitude = model.Latitude;
        destination.Longitude = model.Longitude;
        destination.TravelDistance = model.TravelDistance;

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<DestinationDeleteInputModel?> GetDestinationForDeleteAsync(string? userId, Guid id)
    {
        if (string.IsNullOrEmpty(userId))
            return null;

        return await context.Destinations
            .Include(d => d.Publisher)
            .AsNoTracking()
            .Where(d => d.Id == id && d.PublisherId == userId)
            .Select(d => new DestinationDeleteInputModel
            {
                Id = d.Id,
                Name = d.Name,
                Publisher = d.Publisher != null ? d.Publisher.UserName! : "Unknown"
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteDestinationAsync(Guid id, string userId)
    {
        var destination = await context.Destinations
            .FirstOrDefaultAsync(d => d.Id == id && d.PublisherId == userId);

        if (destination == null)
            return false;

        destination.IsDeleted = true;
        destination.DeletedAt = DateTime.UtcNow;
        destination.DeletedBy = userId;

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> PermanentDeleteDestinationAsync(Guid id)
    {
        var destination = await context.Destinations
            .FirstOrDefaultAsync(d => d.Id == id);

        if (destination == null)
            return false;

        context.Destinations.Remove(destination);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RestoreDestinationAsync(Guid id)
    {
        var destination = await context.Destinations
            .FirstOrDefaultAsync(d => d.Id == id && d.IsDeleted);

        if (destination == null)
            return false;

        destination.IsDeleted = false;
        destination.DeletedAt = null;
        destination.DeletedBy = null;

        return await context.SaveChangesAsync() > 0;
    }
}