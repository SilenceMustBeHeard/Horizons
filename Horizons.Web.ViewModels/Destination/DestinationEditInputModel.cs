
using System.ComponentModel.DataAnnotations;
using Horizons.GCommon;

namespace Horizons.Web.ViewModels.Destination
{
    public class DestinationEditInputModel
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.DestinationNameMinLength)]
        [Display(Name = "Destination Name")]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(ValidationConstants.DestinationDescriptionMinLength)]
        [Display(Name = "Adventure Story")]
        public string Description { get; set; } = null!;

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Date of Adventure")]
        [DataType(DataType.Date)]
        public DateTime PublishedOn { get; set; }

        [Required]
        [Display(Name = "Terrain Type")]
        public Guid TerrainId { get; set; }

        // MAP PROPERTIES
        [Display(Name = "Country")]
        [Required]
        public string Country { get; set; } = string.Empty;

        [Display(Name = "Continent")]
        [Required]
        public string Continent { get; set; } = string.Empty;

        [Display(Name = "Latitude")]
        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Display(Name = "Travel Distance (KM)")]
        public double? TravelDistance { get; set; }

     
        public IEnumerable<AddDestinationTerrainDropdownModel>? Terrains { get; set; }
    }
}