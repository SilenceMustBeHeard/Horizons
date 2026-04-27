namespace Horizons.Web.ViewModels.Map
{
    public class MapDestinationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Country { get; set; }
        public string? Continent { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int? Rank { get; set; }
        public double? Distance { get; set; }
        public string? VisitedDate { get; set; }
    }
}