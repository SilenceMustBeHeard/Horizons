using Horizons.Data.Models.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace Horizons.Data.Models;

public class UserDestination
{
    public string UserId { get; set; } = null!; 
    public virtual AppUser User { get; set; } = null!;

    public Guid DestinationId { get; set; }
    public virtual Destination Destination { get; set; } = null!;
}