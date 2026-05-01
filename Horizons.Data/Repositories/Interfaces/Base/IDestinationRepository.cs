using Horizons.Data.Models;
using Horizons.Data.Repositories.Interfaces.CRUD;

namespace Horizons.Data.Repositories.Interfaces.Base;

public interface IDestinationRepository : IFullRepositoryAsync<Destination, Guid>
{
    // Basic operations
    Task<Destination?> GetByIdIncludingDeletedAsync(Guid id);
    Task<Destination?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<Destination>> GetAllActiveAsync();
    Task<IEnumerable<Destination>> GetAllForAdminAsync();

    // Filtering
    Task<IEnumerable<Destination>> GetByTerrainIdAsync(Guid terrainId);
    Task<IEnumerable<Destination>> GetByContinentAsync(string continent);
    Task<IEnumerable<Destination>> GetByCountryAsync(string country);

    // Pagination
    Task<IEnumerable<Destination>> GetPagedActiveDestinationsAsync(int page, int pageSize);
    Task<int> CountActiveAsync();

    // User specific
    Task<IEnumerable<Destination>> GetUserFavoriteDestinationsAsync(string userId);
    Task<bool> IsUserFavoriteAsync(string userId, Guid destinationId);
    Task<int> GetFavoritesCountAsync(Guid destinationId);

    // Search
    Task<IEnumerable<Destination>> SearchDestinationsAsync(string searchTerm);

    // Map data
    Task<IEnumerable<Destination>> GetMapDestinationsAsync();
}