using Horizons.Web.ViewModels.Destination;
using Horizons.Web.ViewModels.Map;

namespace Horizons.Services.Core.Interfaces
{


    public interface IDestinationService
    {
        Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsAsync(string? userId);



        Task<DestinationDetailsViewModel?> GetDestinationDetailsByIdAsync(int? destinationId, string? userId);

        Task<IEnumerable<DestinationIndexViewModel>> GetTopDestinationsAsync(string? userId, int count);
   
        Task<bool> AddDestinationAsync(DestinationAddInputModel model, string userId);



        public Task<DestinationEditInputModel?> GetDestinationForEditAsync(string? userId, int id);
        Task<bool> EditDestinationAsync(DestinationEditInputModel model, string userId);
        Task<bool> IsUserPublisherAsync(int destinationId, string userId);


        Task<DestinationDeleteInputModel?> GetDestinationForDeleteAsync(string? userId, int id);
        Task<bool> DeleteDestinationAsync(int id, string userId);



        Task<IEnumerable<DestinationFavoriteViewModel>> GetUserFavoriteDestinationsAsync(string userId);
        Task<bool> RemoveFromFavoritesAsync(string userId, int destinationId);
        Task<bool> AddToFavoritesAsync(string userId, int destinationId);
        Task<List<MapDestinationDto>> GetMapDataAsync();


    }


}