namespace Horizons.Web.ViewModels.Destination;

    public class DestinationDeleteViewModel
    {  public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Country { get; set; }
        public string? Continent { get; set; }
        public string? PublishedOn { get; set; }
    }

