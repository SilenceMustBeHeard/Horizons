namespace Horizons.Web.ViewModels
{
    public class BaseDestinationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string TerrainName { get; set; } = null!;
        public bool IsUserPublisher { get; set; }
        public bool IsUserFavourite { get; set; }

        //  properties for map support
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Country { get; set; }
        public string? Continent { get; set; }
    }
}