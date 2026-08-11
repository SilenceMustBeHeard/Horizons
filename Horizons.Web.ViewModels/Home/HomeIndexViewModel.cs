using Horizons.Web.ViewModels.Destination;

namespace Horizons.Web.ViewModels.Home
{

    public class HomeIndexViewModel
    {
        public int TotalDestinations { get; set; }
        public int TotalCountries { get; set; }
        public int TotalStories { get; set; }

        public int MountainsCount { get; set; }
        public int BeachesCount { get; set; }
        public int ForestsCount { get; set; }
        public int UrbanCount { get; set; }
        public int DesertsCount { get; set; }
        public int LakesCount { get; set; }

        public List<FeaturedDestinationViewModel> FeaturedDestinations { get; set; } = new();
    }
}
