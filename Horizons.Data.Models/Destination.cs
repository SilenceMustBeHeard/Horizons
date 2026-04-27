using Horizons.GCommon;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Horizons.Data.Models
{
    public class Destination : BaseDeletableEntity
    {
        [MinLength(ValidationConstants.DestinationNameMinLength)]
        public string Name { get; set; } = null!;

        [MinLength(ValidationConstants.DestinationDescriptionMinLength)]
        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string PublisherId { get; set; } = null!; 
        public virtual AppUser Publisher { get; set; } = null!;

        public Guid TerrainId { get; set; }
        public virtual Terrain Terrain { get; set; } = null!;

        [Display(Name = "Country")]
        public string? Country { get; set; }

        [Display(Name = "Continent")]
        public string? Continent { get; set; }

        [Display(Name = "Latitude")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double? Latitude { get; set; }

        [Display(Name = "Longitude")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double? Longitude { get; set; }

        [Display(Name = "Travel Distance (KM)")]
        public double? TravelDistance { get; set; }

        [Display(Name = "Rating")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? Rating { get; set; }

        // Navigation property for favorites
        public virtual ICollection<UserDestination> UsersDestinations { get; set; } = new HashSet<UserDestination>();
    }
}