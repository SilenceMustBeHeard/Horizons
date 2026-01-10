using Horizons.GCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizons.Web.ViewModels.Destination
{
    public class DestinationAddInputModel
    {
        [Required]
        [MinLength(ValidationConstants.DestinationNameMinLength)]
        [MaxLength(ValidationConstants.DestinationNameMaxLength)]
        public string Name { get; set; } = null!;


        [Required]
        [MinLength(ValidationConstants.DestinationDescriptionMinLength)]
        [MaxLength(ValidationConstants.DestinationDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public int TerrainId { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime PublishedOn { get; set; }

        public IEnumerable<AddDestinationTerrainDropdownModel>? Terrains { get; set; } 




    }
}
