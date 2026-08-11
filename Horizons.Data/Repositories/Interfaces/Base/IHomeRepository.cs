using Horizons.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Data.Repositories.Interfaces.Base;

public interface IHomeRepository
{
    Task<List<Destination>> LoadActiveDestinationsAsync();
    Task<int> GetTotalDestinationsCountAsync();
    Task<int> GetUniqueCountriesCountAsync();
    Task<int> CountByTerrainAsync(string terrainKeyword);
    Task<int> CountByTerrainAsync(string[] terrainKeywords);
    Task<List<Destination>> GetFeaturedDestinationsAsync(int count);
}
