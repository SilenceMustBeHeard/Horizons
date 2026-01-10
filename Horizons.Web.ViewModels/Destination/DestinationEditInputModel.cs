using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizons.Web.ViewModels.Destination
{
    public class DestinationEditInputModel : DestinationAddInputModel
    {
        [Required]
        public int Id { get; set; } 
        
        public IEnumerable<AddDestinationTerrainDropdownModel> Terrains { get; set; }
            = new List<AddDestinationTerrainDropdownModel>();

    }
}
