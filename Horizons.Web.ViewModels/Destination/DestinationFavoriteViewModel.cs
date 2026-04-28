namespace Horizons.Web.ViewModels.Destination
{
    public class DestinationFavoriteViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Terrain { get; set; } = null!;

        public string? ImageUrl { get; set; }

    }
}
