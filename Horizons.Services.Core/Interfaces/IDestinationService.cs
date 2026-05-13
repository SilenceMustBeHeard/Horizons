using Horizons.Web.ViewModels.Destination;
using Horizons.Web.ViewModels.Map;

namespace Horizons.Services.Core.Interfaces;

public interface IDestinationService
{
    // Client-side  (READ ONLY + Favorites)
    Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsAsync(string? userId);
    Task<DestinationDetailsViewModel?> GetDestinationDetailsByIdAsync(Guid? destinationId, string? userId);
    Task<IEnumerable<DestinationIndexViewModel>> GetTopDestinationsAsync(string? userId, int count);
    Task<List<MapDestinationDto>> GetMapDataAsync();

    // Create
    Task<bool> AddDestinationAsync(DestinationAddInputModel model, string userId);
    // Update
    Task<DestinationEditInputModel?> GetDestinationForEditAsync(string? userId, Guid id);
    Task<bool> EditDestinationAsync(DestinationEditInputModel model, string userId);

    // Favorites
    Task<IEnumerable<DestinationFavoriteViewModel>> GetUserFavoriteDestinationsAsync(string userId);
    Task<bool> AddToFavoritesAsync(string userId, Guid destinationId);
    Task<bool> RemoveFromFavoritesAsync(string userId, Guid destinationId);

    // Helper methods
    Task<bool> IsUserPublisherAsync(Guid destinationId, string userId);
}