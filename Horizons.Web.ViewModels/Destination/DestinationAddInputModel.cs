
using System.ComponentModel.DataAnnotations;
using Horizons.GCommon;

namespace Horizons.Web.ViewModels.Destination
{
    public class DestinationAddInputModel
    {
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

        // NEW MAP PROPERTIES
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required for map display")]
        public string Country { get; set; } = string.Empty;

        [Display(Name = "Continent")]
        [Required(ErrorMessage = "Continent is required for map display")]
        public string Continent { get; set; } = string.Empty;

        [Display(Name = "Latitude")]
        [Required(ErrorMessage = "Latitude is required for map pin")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        [Required(ErrorMessage = "Longitude is required for map pin")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double Longitude { get; set; }

        [Display(Name = "Travel Distance (KM)")]
        public double? TravelDistance { get; set; }

     
        public IEnumerable<AddDestinationTerrainDropdownModel>? Terrains { get; set; }
    }
}