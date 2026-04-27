using Horizons.GCommon;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Horizons.Data.Models
{
    public class Destination
    {
        public int Id { get; set; }

        [MinLength(ValidationConstants.DestinationNameMinLength)]
        public string Name { get; set; } = null!;

        [MinLength(ValidationConstants.DestinationDescriptionMinLength)]
        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string PublisherId { get; set; } = null!;

        public virtual IdentityUser Publisher { get; set; } = null!;

        public DateTime PublishedOn { get; set; }

        public int TerrainId { get; set; }

        public virtual Terrain Terrain { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;



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

       
        //public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        
        public virtual ICollection<UserDestination> Favorites => UsersDestinations;

        public virtual ICollection<UserDestination> UsersDestinations { get; set; } = new HashSet<UserDestination>();
    }
}