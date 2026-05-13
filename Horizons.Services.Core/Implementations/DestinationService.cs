using Horizons.Data;
using Horizons.Data.Models;
using Horizons.Data.Models.Base;
using Horizons.Data.Repositories.Interfaces.Base;
using Horizons.Services.Core.Interfaces;
using Horizons.Web.ViewModels.Destination;
using Horizons.Web.ViewModels.Map;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Services.Core.Implementations;

public class DestinationService : IDestinationService
{
    protected readonly UserManager<AppUser> _userManager;
    protected readonly IDestinationRepository _destinationRepository;

    public DestinationService(UserManager<AppUser> userManager, IDestinationRepository destinationRepository)
    {
        _userManager = userManager;
        _destinationRepository = destinationRepository;
    }

    public async Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsAsync(string? userId)
    {
        var query = _destinationRepository.Query()
            .Where(d => !d.IsDeleted)
            .Include(d => d.Terrain)
            .Include(d => d.Favorites);

        var destinations = await query.ToListAsync();

        return destinations.Select(d => new DestinationIndexViewModel
        {
            Id = d.Id,
            Name = d.Name,
            ImageUrl = d.ImageUrl,
            TerrainName = d.Terrain?.Name ?? string.Empty,
            FavouriteCount = d.Favorites.Count(f => !f.IsDeleted),
            IsUserPublisher = d.PublisherId == userId,
            IsUserFavourite = userId != null && d.Favorites.Any(f => f.UserId == userId && !f.IsDeleted)
        });
    }

    public async Task<DestinationDetailsViewModel?> GetDestinationDetailsByIdAsync(Guid? id, string? userId)
    {
        if (!id.HasValue) return null;

        var destination = await _destinationRepository.Query()
            .Where(d => d.Id == id && !d.IsDeleted)
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .Include(d => d.Favorites)
            .FirstOrDefaultAsync();

        if (destination == null) return null;

        return new DestinationDetailsViewModel
        {
            Id = destination.Id,
            Name = destination.Name,
            Description = destination.Description,
            ImageUrl = destination.ImageUrl,
            TerrainName = destination.Terrain?.Name ?? string.Empty,
            PublishedOn = destination.CreatedAt.ToString("MMMM dd, yyyy"),
            PublisherName = destination.Publisher?.FullName ?? "Unknown",
            IsUserPublisher = destination.PublisherId == userId,
            IsUserFavourite = userId != null && destination.Favorites.Any(f => f.UserId == userId && !f.IsDeleted),
            FavoriteCount = destination.Favorites.Count(f => !f.IsDeleted),
            Latitude = destination.Latitude,
            Longitude = destination.Longitude,
            Country = destination.Country,
            Continent = destination.Continent
        };
    }

    public async Task<IEnumerable<DestinationIndexViewModel>> GetTopDestinationsAsync(string? userId, int count)
    {
        var topDestinations = await _destinationRepository.Query()
            .Where(d => !d.IsDeleted)
            .Include(d => d.Terrain)
            .Include(d => d.Favorites)
            .OrderByDescending(d => d.Favorites.Count(f => !f.IsDeleted))
            .Take(count)
            .ToListAsync();

        return topDestinations.Select(d => new DestinationIndexViewModel
        {
            Id = d.Id,
            Name = d.Name,
            ImageUrl = d.ImageUrl,
            TerrainName = d.Terrain?.Name ?? string.Empty,
            FavouriteCount = d.Favorites.Count(f => !f.IsDeleted),
            IsUserPublisher = d.PublisherId == userId,
            IsUserFavourite = userId != null && d.Favorites.Any(f => f.UserId == userId && !f.IsDeleted)
        });
    }

    public async Task<List<MapDestinationDto>> GetMapDataAsync()
    {
        return await _destinationRepository.Query()
            .Where(d => !d.IsDeleted && d.Latitude.HasValue && d.Longitude.HasValue)
            .Select(d => new MapDestinationDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                ImageUrl = d.ImageUrl,
                Country = d.Country ?? string.Empty,
                Continent = d.Continent ?? string.Empty,
                Latitude = d.Latitude,
                Longitude = d.Longitude,
                Likes = d.Favorites.Count(f => !f.IsDeleted),
                
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<DestinationFavoriteViewModel>> GetUserFavoriteDestinationsAsync(string userId)
    {
        var favorites = await _destinationRepository.Query()
            .Where(d => !d.IsDeleted && d.Favorites.Any(f => f.UserId == userId && !f.IsDeleted))
            .Include(d => d.Terrain)
            .Select(d => new DestinationFavoriteViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Terrain = d.Terrain != null ? d.Terrain.Name : string.Empty,
                ImageUrl = d.ImageUrl
            })
            .ToListAsync();

        return favorites;
    }

    public async Task<bool> AddToFavoritesAsync(string userId, Guid destinationId)
    {
        var existing = await _destinationRepository.Query()
            .Where(d => d.Id == destinationId)
            .SelectMany(d => d.Favorites)
            .AnyAsync(f => f.UserId == userId && !f.IsDeleted);

        if (existing) return false;

        var favorite = new Favorite
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            DestinationId = destinationId,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await _destinationRepository.AddFavoriteAsync(favorite);
        return true;
    }

    public async Task<bool> RemoveFromFavoritesAsync(string userId, Guid destinationId)
    {
        return await _destinationRepository.RemoveFromFavoritesAsync(userId, destinationId);
    }

    public async Task<bool> IsUserPublisherAsync(Guid destinationId, string userId)
    {
        if (string.IsNullOrEmpty(userId)) return false;

        var destination = await _destinationRepository.Query()
            .Where(d => d.Id == destinationId && !d.IsDeleted)
            .Select(d => new { d.PublisherId })
            .FirstOrDefaultAsync();

        return destination?.PublisherId == userId;
    }
}