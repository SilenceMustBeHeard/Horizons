using Horizons.Data;
using Horizons.Data.Models;
using Horizons.Data.Models.Base;
using Horizons.Services.Core.Interfaces;
using Horizons.Web.ViewModels.Destination;
using Horizons.Web.ViewModels.Map;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Services.Core.Implementations;

public class DestinationService : IDestinationService
{
    private readonly AppDbContext context;
    private readonly UserManager<AppUser> userManager;

    public DestinationService(AppDbContext context,
        UserManager<AppUser> userManager)
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

    public async Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsAsync(string? userId)
    {
        bool isUserValid = !string.IsNullOrEmpty(userId);

        return await context.Destinations
            .Include(d => d.Terrain)
            .Include(d => d.UsersDestinations)
            .Where(d => !d.IsDeleted)
            .AsNoTracking()
            .Select(d => new DestinationIndexViewModel
            {
                Id = d.Id,
                Name = d.Name,
                ImageUrl = d.ImageUrl,
                TerrainName = d.Terrain.Name,
                FavouriteCount = d.UsersDestinations.Count,
                IsUserPublisher = isUserValid && d.PublisherId == userId,
                IsUserFavourite = isUserValid && d.UsersDestinations.Any(ud => ud.UserId == userId)
            })
            .ToListAsync();
    }

    public async Task<DestinationDetailsViewModel?> GetDestinationDetailsByIdAsync(Guid? id, string? userId)
    {
        if (!id.HasValue)
            return null;

        var destination = await context.Destinations
            .Include(d => d.Terrain)
            .Include(d => d.UsersDestinations)
            .Include(d => d.Publisher)
            .Where(d => !d.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        if (destination == null)
            return null;

        bool isUserValid = !string.IsNullOrEmpty(userId);

        return new DestinationDetailsViewModel
        {
            Id = destination.Id,
            Name = destination.Name,
            Description = destination.Description,
            ImageUrl = destination.ImageUrl,
            TerrainName = destination.Terrain.Name,
            PublishedOn = destination.CreatedAt.ToString("dd.MM.yyyy"),
            PublisherName = destination.Publisher?.UserName ?? "Unknown",
            IsUserPublisher = isUserValid && destination.PublisherId == userId,
            IsUserFavourite = isUserValid && destination.UsersDestinations.Any(ud => ud.UserId == userId),
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
            .Where(d => d.Id == id && d.PublisherId == userId && !d.IsDeleted)
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

    public async Task<bool> IsUserPublisherAsync(Guid destinationId, string userId)
    {
        var destination = await context.Destinations
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == destinationId && !d.IsDeleted);

        if (destination == null)
            return false;

        return destination.PublisherId == userId;
    }

    public async Task<DestinationDeleteInputModel?> GetDestinationForDeleteAsync(string? userId, Guid id)
    {
        if (string.IsNullOrEmpty(userId))
            return null;

        return await context.Destinations
            .Include(d => d.Publisher)
            .AsNoTracking()
            .Where(d => d.Id == id && d.PublisherId == userId && !d.IsDeleted)
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

    public async Task<bool> AddToFavoritesAsync(string userId, Guid destinationId)
    {
        var destination = await context.Destinations
            .FirstOrDefaultAsync(d => d.Id == destinationId && !d.IsDeleted);

        if (destination == null)
            return false;

        bool alreadyFavorited = await context.UsersDestinations
            .AnyAsync(ud => ud.UserId == userId && ud.DestinationId == destinationId);

        if (alreadyFavorited)
            return false;

        var userDestination = new UserDestination
        {
            UserId = userId,
            DestinationId = destinationId
        };

        await context.UsersDestinations.AddAsync(userDestination);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveFromFavoritesAsync(string userId, Guid destinationId)
    {
        var entry = await context.UsersDestinations
            .FirstOrDefaultAsync(ud => ud.UserId == userId && ud.DestinationId == destinationId);

        if (entry == null)
            return false;

        context.UsersDestinations.Remove(entry);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<DestinationFavoriteViewModel>> GetUserFavoriteDestinationsAsync(string userId)
    {
        return await context.UsersDestinations
            .Where(ud => ud.UserId == userId)
            .Include(ud => ud.Destination)
                .ThenInclude(d => d.Terrain)
            .Where(ud => !ud.Destination.IsDeleted)
            .Select(ud => new DestinationFavoriteViewModel
            {
                Id = ud.Destination.Id,
                Name = ud.Destination.Name,
                Terrain = ud.Destination.Terrain.Name,
                ImageUrl = ud.Destination.ImageUrl
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<DestinationIndexViewModel>> GetTopDestinationsAsync(string? userId, int count)
    {
        bool isUserValid = !string.IsNullOrEmpty(userId);

        return await context.Destinations
            .Include(d => d.Terrain)
            .Include(d => d.UsersDestinations)
            .Where(d => !d.IsDeleted)
            .OrderByDescending(d => d.UsersDestinations.Count)
            .Take(count)
            .Select(d => new DestinationIndexViewModel
            {
                Id = d.Id,
                Name = d.Name,
                ImageUrl = d.ImageUrl,
                TerrainName = d.Terrain.Name,
                FavouriteCount = d.UsersDestinations.Count,
                IsUserPublisher = isUserValid && d.PublisherId == userId,
                IsUserFavourite = isUserValid && d.UsersDestinations.Any(ud => ud.UserId == userId)
            })
            .ToListAsync();
    }

    public async Task<List<MapDestinationDto>> GetMapDataAsync()
    {
        return await context.Destinations
            .Where(d => d.Latitude != null && d.Longitude != null && !d.IsDeleted)
            .Include(d => d.UsersDestinations)
            .Select(d => new MapDestinationDto
            {
                Id = d.Id,
                Name = d.Name,
                Country = d.Country ?? "Unknown",
                Continent = d.Continent ?? "Unknown",
                Latitude = d.Latitude,
                Longitude = d.Longitude,
                Description = d.Description,
                ImageUrl = d.ImageUrl,
                CreatedAt = d.CreatedAt,
                Likes = d.UsersDestinations.Count,
                Comments = 0,
                Rank = d.Rating,
                Distance = d.TravelDistance,
                VisitedDate = d.CreatedAt.ToString("yyyy-MM-dd")
            })
            .ToListAsync();
    }
}