using Horizons.Data;

using Horizons.Services.Core.Interfaces;
using Horizons.Web.ViewModels.Destination;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Services.Core.Implementations
{
    public class TerrainService : ITerrainService
    {
        private readonly AppDbContext dbContext;


        public TerrainService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<AddDestinationTerrainDropdownModel>> GetAllTerrainsDropdownAsync()
        {
            var terrains = dbContext.Terrains
                .AsNoTracking()
                .Select(t => new AddDestinationTerrainDropdownModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToArrayAsync();

            return await terrains;
        }
    }
}
