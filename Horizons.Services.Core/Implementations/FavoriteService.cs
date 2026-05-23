using Horizons.Data.Models;
using Horizons.Data.Repositories.Interfaces.Interactions;
using Horizons.Services.Core.Interfaces;
using Horizons.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Services.Core.Implementations;


public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;

    public FavoriteService(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    public async Task<bool> ToggleFavoriteAsync(string userId, Guid destinationId)
    {
        var favorite = await _favoriteRepository.GetByCompositeKeyAsync(userId, destinationId);

        if (favorite == null)
        {
            // Add new favorite
            await _favoriteRepository.AddAsync(new Favorite
            {
                UserId = userId,
                DestinationId = destinationId
            });
            await _favoriteRepository.SaveChangesAsync();
            return true; // Now favorited
        }

        // Toggle soft delete
        favorite.IsDeleted = !favorite.IsDeleted;
        await _favoriteRepository.UpdateAsync(favorite);
        await _favoriteRepository.SaveChangesAsync();

        return !favorite.IsDeleted; // Returns true if now favorited, false if removed
    }
    public async Task<IEnumerable<BaseDestinationViewModel>> GetUserFavoritesAsync(string userId)
    {
        var favorites = await _favoriteRepository
            .Query()
            .Where(f => f.UserId == userId && !f.IsDeleted)
            .Include(f => f.Destination)
                .ThenInclude(d => d.TerrainId)
            .ToListAsync();

        return favorites.Select(f => new BaseDestinationViewModel
        {
            Id = f.Destination.Id,
            Name = f.Destination.Name,
            ImageUrl = f.Destination.ImageUrl,
            TerrainName = f.Destination.Terrain.Name,
            Latitude = f.Destination.Latitude,
            Longitude = f.Destination.Longitude,
            Country = f.Destination.Country,
            Continent = f.Destination.Continent


        });
    }
    public async Task<bool> IsFavoriteAsync(string userId, Guid destinationId)
    {
        return await _favoriteRepository.ExistsAsync(userId, destinationId);
    }
}