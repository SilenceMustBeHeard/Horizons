using Horizons.Data.Models;
using Horizons.Data.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horizons.Data.Repositories.Implementations.Base;

public class TerrainRepository : RepositoryAsync<Terrain, Guid>, ITerrainRepository
{
    private readonly AppDbContext _context;

    public TerrainRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    

    public async Task<Terrain?> GetByIdIncludingDeletedAsync(Guid id)
    {
        return await _dbSet
            .IgnoreQueryFilters()
            .Include(t => t.Destinations)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Terrain>> GetAllActiveAsync()
    {
        return await _dbSet
            .Where(t => !t.IsDeleted)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Terrain>> GetAllForAdminAsync()
    {
        return await _dbSet
            .IgnoreQueryFilters()
            .Include(t => t.Destinations)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task ToggleTerrainStatusAsync(Terrain terrain)
    {
        terrain.IsDeleted = !terrain.IsDeleted;
        terrain.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(terrain);
        await _context.SaveChangesAsync();
    }

    public Terrain? GetByName(string name)
    {
        return _dbSet
            .FirstOrDefault(t => t.Name == name && !t.IsDeleted);
    }

    public async Task<Terrain?> GetByNameIncludingDeletedAsync(string name)
    {
        return await _dbSet
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(t => t.Name == name);
    }

    public async Task<int> GetDestinationsCountAsync(Guid terrainId)
    {
        return await _context.Destinations
            .CountAsync(d => d.TerrainId == terrainId && !d.IsDeleted);
    }

    public async Task<bool> HasDestinationsAsync(Guid terrainId)
    {
        return await _context.Destinations
            .AnyAsync(d => d.TerrainId == terrainId && !d.IsDeleted);
    }

    public async Task<bool> CanDeleteTerrainAsync(Guid terrainId)
    {
        return !await HasDestinationsAsync(terrainId);
    }

    public async Task<bool> SafeDeleteTerrainAsync(Guid terrainId)
    {
        if (!await CanDeleteTerrainAsync(terrainId))
            return false;

        var terrain = await GetByIdAsync(terrainId);
        if (terrain == null)
            return false;

        await ToggleTerrainStatusAsync(terrain);
        return true;
    }
}