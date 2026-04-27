using Horizons.GCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizons.Data.Models
{
    public class Terrain : BaseDeletableEntity
    {
  

        [MinLength(ValidationConstants.TerrainNameMinLength)]
        public string Name { get; set; } = null!;

    

        public virtual ICollection<Destination> Destinations 
        { get; set; } = new HashSet<Destination>();

    }
}
