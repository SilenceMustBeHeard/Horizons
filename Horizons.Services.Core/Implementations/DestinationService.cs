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
    protected readonly AppDbContext context;
    protected readonly UserManager<AppUser> userManager;

    public DestinationService(AppDbContext context, UserManager<AppUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    public async Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsAsync(string? userId)
    {
        bool isUserValid = !string.IsNullOrEmpty(userId);

        return await context.Destinations
            .Include(d => d.Terrain)
            .Include(d => d.Favorites)
            .Where(d => !d.IsDeleted)
            .AsNoTracking()
            .Select(d => new DestinationIndexViewModel
            {
                Id = d.Id,
                Name = d.Name,
                ImageUrl = d.ImageUrl,
                TerrainName = d.Terrain.Name,
                FavouriteCount = d.Favorites.Count,
                IsUserPublisher = isUserValid && d.PublisherId == userId,
                IsUserFavourite = isUserValid && d.Favorites.Any(f => f.UserId == userId)
            })
            .ToListAsync();
    }

    public async Task<DestinationDetailsViewModel?> GetDestinationDetailsByIdAsync(Guid? id, string? userId)
    {
        if (!id.HasValue)
            return null;

        var destination = await context.Destinations
            .Include(d => d.Terrain)
            .Include(d => d.Favorites)
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
            IsUserFavourite = isUserValid && destination.Favorites.Any(f => f.UserId == userId),
            FavoriteCount = destination.Favorites.Count,
            Latitude = destination.Latitude,
            Longitude = destination.Longitude,
            Country = destination.Country,
            Continent = destination.Continent
        };
    }

    public async Task<IEnumerable<DestinationIndexViewModel>> GetTopDestinationsAsync(string? userId, int count)
    {
        bool isUserValid = !string.IsNullOrEmpty(userId);

        return await context.Destinations
            .Include(d => d.Terrain)
            .Include(d => d.Favorites)
            .Where(d => !d.IsDeleted)
            .OrderByDescending(d => d.Favorites.Count)
            .Take(count)
            .Select(d => new DestinationIndexViewModel
            {
                Id = d.Id,
                Name = d.Name,
                ImageUrl = d.ImageUrl,
                TerrainName = d.Terrain.Name,
                FavouriteCount = d.Favorites.Count,
                IsUserPublisher = isUserValid && d.PublisherId == userId,
                IsUserFavourite = isUserValid && d.Favorites.Any(f => f.UserId == userId)
            })
            .ToListAsync();
    }

    public async Task<List<MapDestinationDto>> GetMapDataAsync()
    {
        return await context.Destinations
            .Where(d => d.Latitude != null && d.Longitude != null && !d.IsDeleted)
            .Include(d => d.Favorites)
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
                Likes = d.Favorites.Count,
                Comments = 0,
                Rank = d.Rating,
                Distance = d.TravelDistance,
                VisitedDate = d.CreatedAt.ToString("yyyy-MM-dd")
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<DestinationFavoriteViewModel>> GetUserFavoriteDestinationsAsync(string userId)
    {
        return await context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.Destination)
                .ThenInclude(d => d.Terrain)
            .Where(f => !f.Destination.IsDeleted)
            .Select(f => new DestinationFavoriteViewModel
            {
                Id = f.Destination.Id,
                Name = f.Destination.Name,
                Terrain = f.Destination.Terrain.Name,
                ImageUrl = f.Destination.ImageUrl
            })
            .ToListAsync();
    }

    public async Task<bool> AddToFavoritesAsync(string userId, Guid destinationId)
    {
        var destination = await context.Destinations
            .FirstOrDefaultAsync(d => d.Id == destinationId && !d.IsDeleted);

        if (destination == null)
            return false;

        bool alreadyFavorited = await context.Favorites
            .AnyAsync(f => f.UserId == userId && f.DestinationId == destinationId);

        if (alreadyFavorited)
            return false;

        var favorite = new Favorite
        {
            UserId = userId,
            DestinationId = destinationId
        };

        await context.Favorites.AddAsync(favorite);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveFromFavoritesAsync(string userId, Guid destinationId)
    {
        var entry = await context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.DestinationId == destinationId);

        if (entry == null)
            return false;

        context.Favorites.Remove(entry);
        return await context.SaveChangesAsync() > 0;
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
}