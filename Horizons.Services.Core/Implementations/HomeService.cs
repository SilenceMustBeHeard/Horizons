using Horizons.Data.Repositories.Interfaces.Base;
using Horizons.Services.Core.Interfaces;
using Horizons.Web.ViewModels.Home;

namespace Horizons.Services.Core.Implementations;

public class HomeService : IHomeService
{
    private readonly IHomeRepository _homeRepository;

    public HomeService(IHomeRepository homeRepository)
    {
        _homeRepository = homeRepository;
    }

    public async Task<HomeIndexViewModel> GetHomePageDataAsync()
    {
        var allDestinations = await _homeRepository.LoadActiveDestinationsAsync();

        var featuredDestinations = allDestinations
            .OrderByDescending(d => d.Favorites?.Count ?? 0)
            .Take(6)
            .Select(d => new FeaturedDestinationViewModel
            {
                Id = d.Id,
                Name = d.Name,
                ImageUrl = d.ImageUrl,
                Country = d.Country,
                Description = TruncateDescription(d.Description, 150),
                Likes = d.Favorites?.Count ?? 0,
                CreatedAt = d.CreatedAt,
                TerrainName = d.Terrain?.Name
            })
            .ToList();

        return new HomeIndexViewModel
        {
            TotalDestinations = allDestinations.Count,
            TotalCountries = allDestinations
                .Select(d => d.Country)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .Count(),
            TotalStories = allDestinations.Count,
            MountainsCount = allDestinations.Count(d => d.Terrain != null && d.Terrain.Name.Contains("mountain")),
            BeachesCount = allDestinations.Count(d => d.Terrain != null && d.Terrain.Name.Contains("beach")),
            ForestsCount = allDestinations.Count(d => d.Terrain != null && d.Terrain.Name.Contains("forest")),
            UrbanCount = allDestinations.Count(d => d.Terrain != null && (d.Terrain.Name.Contains("urban") || d.Terrain.Name.Contains("city"))),
            DesertsCount = allDestinations.Count(d => d.Terrain != null && d.Terrain.Name.Contains("desert")),
            LakesCount = allDestinations.Count(d => d.Terrain != null && d.Terrain.Name.Contains("lake")),
            FeaturedDestinations = featuredDestinations
        };
    }

    private string TruncateDescription(string description, int maxLength)
    {
        if (string.IsNullOrEmpty(description)) return description;
        return description.Length > maxLength ? description.Substring(0, maxLength) : description;
    }
}
