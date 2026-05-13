using Horizons.Data;
using Horizons.Data.Repositories.Interfaces.Base;
using Horizons.Services.Core.Interfaces;
using Horizons.Web.ViewModels.Destination;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Services.Core.Implementations
{
    public class TerrainService : ITerrainService
    {

        private readonly ITerrainRepository _terrainRepository;

        public TerrainService(ITerrainRepository terrainRepository)
        {
            _terrainRepository = terrainRepository;
        }
        public async Task<IEnumerable<AddDestinationTerrainDropdownModel>> GetAllTerrainsDropdownAsync()
        {
            return await _terrainRepository.Query()
                .Where(t => !t.IsDeleted)
                .Select(t => new AddDestinationTerrainDropdownModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }
    }
}
