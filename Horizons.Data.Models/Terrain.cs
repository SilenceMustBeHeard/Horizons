using Horizons.Data.Models.Base;
using Horizons.GCommon;
using System.ComponentModel.DataAnnotations;

namespace Horizons.Data.Models
{
    public class Terrain : BaseDeletableEntity
    {


        [MinLength(ValidationConstants.TerrainNameMinLength)]
        public string Name { get; set; } = null!;



        public virtual ICollection<Destination> Destinations { get; set; }
         = new HashSet<Destination>();

    }
}
