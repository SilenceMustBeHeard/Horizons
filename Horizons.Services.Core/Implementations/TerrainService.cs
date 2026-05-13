using Horizons.Data;

using Horizons.Services.Core.Interfaces;
using Horizons.Web.ViewModels.Destination;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Services.Core.Implementations
{
    public class TerrainService : ITerrainService
    {
      


        public TerrainService(AppDbContext dbContext)
        {
           
        }
        public async Task<IEnumerable<AddDestinationTerrainDropdownModel>> GetAllTerrainsDropdownAsync()
        {
            
        }
    }
}
