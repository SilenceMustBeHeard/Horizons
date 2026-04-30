using Horizons.Data.Models;
using Horizons.Data.Repositories.Interfaces.CRUD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horizons.Data.Repositories.Interfaces.Base;

public interface ITerrainRepository : IFullRepositoryAsync<Terrain, Guid>
{
    Task<Terrain?> GetByIdIncludingDeletedAsync(Guid id);
    Task<IEnumerable<Terrain>> GetAllActiveAsync();
    Task<IEnumerable<Terrain>> GetAllForAdminAsync();
    Task ToggleTerrainStatusAsync(Terrain terrain);
    Terrain? GetByName(string name);


    Task<Terrain?> GetByNameIncludingDeletedAsync(string name);
    Task<int> GetDestinationsCountAsync(Guid terrainId);
    Task<bool> HasDestinationsAsync(Guid terrainId);
    Task<bool> CanDeleteTerrainAsync(Guid terrainId);
    Task<bool> SafeDeleteTerrainAsync(Guid terrainId);
}