using Horizons.Web.ViewModels.Destination;

namespace Horizons.Services.Core.Interfaces
{
    public interface ITerrainService
    {
        Task<IEnumerable<AddDestinationTerrainDropdownModel>> GetAllTerrainsDropdownAsync();




    }
}
