using Horizons.Data;

using Horizons.Services.Core.Interfaces;
using Horizons.Web.ViewModels.Destination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizons.Services.Core.Implementations
{
    public class TerrainService : ITerrainService
    {
        private readonly ApplicationDbContext dbContext;


        public TerrainService(ApplicationDbContext dbContext)
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
