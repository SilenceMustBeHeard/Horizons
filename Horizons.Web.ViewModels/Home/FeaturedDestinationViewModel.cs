using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Web.ViewModels.Home;

public class FeaturedDestinationViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? Country { get; set; }
    public string? Description { get; set; }
    public int Likes { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? TerrainName { get; set; }
}