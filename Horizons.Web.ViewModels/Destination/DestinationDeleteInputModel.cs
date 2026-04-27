using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizons.Web.ViewModels.Destination
{
    public class DestinationDeleteInputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public string Publisher { get; set; } = null!;
    }
}
