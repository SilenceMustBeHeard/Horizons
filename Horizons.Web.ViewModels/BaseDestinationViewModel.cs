using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizons.Web.ViewModels
{
    public class BaseDestinationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }

        public string TerrainName { get; set; } = null!;

       
        public bool IsUserPublisher { get; set; }

        public bool IsUserFavourite { get; set; }

    }
}
