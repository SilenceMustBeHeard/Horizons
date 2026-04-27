using Horizons.Web.ViewModels.Destination;
using Horizons.Web.ViewModels.Map;

namespace Horizons.Services.Core.Interfaces
{
    public interface IDestinationService
    {
        Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsAsync(string? userId);
        Task<DestinationDetailsViewModel?> GetDestinationDetailsByIdAsync(Guid? destinationId, string? userId);
        Task<IEnumerable<DestinationIndexViewModel>> GetTopDestinationsAsync(string? userId, int count);
        Task<bool> AddDestinationAsync(DestinationAddInputModel model, string userId);
        Task<DestinationEditInputModel?> GetDestinationForEditAsync(string? userId, Guid id);
        Task<bool> EditDestinationAsync(DestinationEditInputModel model, string userId);
        Task<bool> IsUserPublisherAsync(Guid destinationId, string userId);
        Task<DestinationDeleteInputModel?> GetDestinationForDeleteAsync(string? userId, Guid id);
        Task<bool> DeleteDestinationAsync(Guid id, string userId);
        Task<IEnumerable<DestinationFavoriteViewModel>> GetUserFavoriteDestinationsAsync(string userId);
        Task<bool> RemoveFromFavoritesAsync(string userId, Guid destinationId);
        Task<bool> AddToFavoritesAsync(string userId, Guid destinationId);
        Task<List<MapDestinationDto>> GetMapDataAsync();
    }
}