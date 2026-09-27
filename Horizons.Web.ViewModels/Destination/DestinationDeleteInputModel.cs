namespace Horizons.Web.ViewModels.Destination;

public class DestinationDeleteInputModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Publisher { get; set; }
    public string? ImageUrl { get; set; }
    public string? Country { get; set; }
    public string? Continent { get; set; }
}