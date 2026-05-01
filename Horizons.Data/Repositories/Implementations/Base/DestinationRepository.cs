using Horizons.Data.Models;
using Horizons.Data.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Data.Repositories.Implementations.Base;

public class DestinationRepository : RepositoryAsync<Destination, Guid>, IDestinationRepository
{
    private readonly AppDbContext _context;

    public DestinationRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    // Get by ID including soft-deleted
    public async Task<Destination?> GetByIdIncludingDeletedAsync(Guid id)
        => await _dbSet
            .IgnoreQueryFilters()
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .Include(d => d.Favorites)
            .FirstOrDefaultAsync(d => d.Id == id);

    // Get by ID with all details
    public async Task<Destination?> GetByIdWithDetailsAsync(Guid id)
        => await _dbSet
            .Where(d => !d.IsDeleted)
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .Include(d => d.Favorites)
                .ThenInclude(f => f.User)
            .FirstOrDefaultAsync(d => d.Id == id);

    // Get all active destinations
    public async Task<IEnumerable<Destination>> GetAllActiveAsync()
        => await _dbSet
            .Where(d => !d.IsDeleted)
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .OrderBy(d => d.Name)
            .ToListAsync();

    // Get all for admin (including soft-deleted)
    public async Task<IEnumerable<Destination>> GetAllForAdminAsync()
        => await _dbSet
            .IgnoreQueryFilters()
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .OrderBy(d => d.Name)
            .ToListAsync();

    // Get by terrain
    public async Task<IEnumerable<Destination>> GetByTerrainIdAsync(Guid terrainId)
        => await _dbSet
            .Where(d => d.TerrainId == terrainId && !d.IsDeleted)
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .OrderBy(d => d.Name)
            .ToListAsync();

    // Get by continent
    public async Task<IEnumerable<Destination>> GetByContinentAsync(string continent)
        => await _dbSet
            .Where(d => d.Continent == continent && !d.IsDeleted)
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .OrderBy(d => d.Name)
            .ToListAsync();

    // Get by country
    public async Task<IEnumerable<Destination>> GetByCountryAsync(string country)
        => await _dbSet
            .Where(d => d.Country == country && !d.IsDeleted)
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .OrderBy(d => d.Name)
            .ToListAsync();

    // Paged active destinations
    public async Task<IEnumerable<Destination>> GetPagedActiveDestinationsAsync(int page, int pageSize)
        => await _dbSet
            .Where(d => !d.IsDeleted)
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .OrderBy(d => d.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    // Count active destinations
    public async Task<int> CountActiveAsync()
        => await _dbSet
            .CountAsync(d => !d.IsDeleted);

    // Get user's favorite destinations
    public async Task<IEnumerable<Destination>> GetUserFavoriteDestinationsAsync(string userId)
        => await _context.Favorites
            .Where(f => f.UserId == userId && !f.IsDeleted && !f.Destination.IsDeleted)
            .Include(f => f.Destination)
                .ThenInclude(d => d.Terrain)
            .Include(f => f.Destination)
                .ThenInclude(d => d.Publisher)
            .Select(f => f.Destination)
            .OrderBy(d => d.Name)
            .ToListAsync();

    // Check if destination is in user's favorites
    public async Task<bool> IsUserFavoriteAsync(string userId, Guid destinationId)
        => await _context.Favorites
            .AnyAsync(f => f.UserId == userId && f.DestinationId == destinationId && !f.IsDeleted);

    // Get favorites count for a destination
    public async Task<int> GetFavoritesCountAsync(Guid destinationId)
        => await _context.Favorites
            .CountAsync(f => f.DestinationId == destinationId && !f.IsDeleted);

    // Search destinations
    public async Task<IEnumerable<Destination>> SearchDestinationsAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllActiveAsync();

        return await _dbSet
            .Where(d => !d.IsDeleted &&
                (d.Name.Contains(searchTerm) ||
                 d.Description.Contains(searchTerm) ||
                 d.Country!.Contains(searchTerm) ||
                 d.Continent!.Contains(searchTerm)))
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    // Get destinations for map (only with coordinates)
    public async Task<IEnumerable<Destination>> GetMapDestinationsAsync()
        => await _dbSet
            .Where(d => !d.IsDeleted && d.Latitude.HasValue && d.Longitude.HasValue)
            .Include(d => d.Terrain)
            .Include(d => d.Favorites)
            .Select(d => new Destination
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                ImageUrl = d.ImageUrl,
                Country = d.Country,
                Continent = d.Continent,
                Latitude = d.Latitude,
                Longitude = d.Longitude,
                Terrain = d.Terrain,
                Favorites = d.Favorites,
                Rating = d.Rating
            })
            .ToListAsync();
}