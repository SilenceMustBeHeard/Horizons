using Horizons.Data.Models;
using Horizons.Data.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Data.Repositories.Implementations.Base;

public class HomeRepository : IHomeRepository
{
    private readonly AppDbContext _context;

    public HomeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Destination>> LoadActiveDestinationsAsync()
    {
        return await _context.Destinations
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .Where(d => !d.IsDeleted)
            .ToListAsync();
    }

    public async Task<int> GetTotalDestinationsCountAsync()
    {
        return await _context.Destinations
            .Where(d => !d.IsDeleted)
            .CountAsync();
    }

    public async Task<int> GetUniqueCountriesCountAsync()
    {
        return await _context.Destinations
            .Where(d => !d.IsDeleted && !string.IsNullOrEmpty(d.Country))
            .Select(d => d.Country)
            .Distinct()
            .CountAsync();
    }

    public async Task<int> CountByTerrainAsync(string terrainKeyword)
    {
        return await _context.Destinations
            .Where(d => !d.IsDeleted && d.Terrain != null &&
                d.Terrain.Name.ToLower().Contains(terrainKeyword))
            .CountAsync();
    }

    public async Task<int> CountByTerrainAsync(string[] terrainKeywords)
    {
        return await _context.Destinations
            .Where(d => !d.IsDeleted && d.Terrain != null &&
                terrainKeywords.Any(k => d.Terrain.Name.ToLower().Contains(k)))
            .CountAsync();
    }

    public async Task<List<Destination>> GetFeaturedDestinationsAsync(int count)
    {
        return await _context.Destinations
            .Include(d => d.Terrain)
            .Include(d => d.Publisher)
            .Where(d => !d.IsDeleted)
            .OrderByDescending(d => d.Favorites.Count)
            .Take(count)
            .ToListAsync();
    }
}