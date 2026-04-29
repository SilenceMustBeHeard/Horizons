using Horizons.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Data.Models;

public class Favorite : BaseDeletableEntity
{

    public string UserId { get; set; } = null!;
    public virtual AppUser User { get; set; } = null!;


    public Guid DestinationId { get; set; }
    public virtual Destination Destination { get; set; } = null!;





}