using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Horizons.Data.Models;

public class AppUser : IdentityUser
{
    public string FullName => $"{FirstName?.Trim()} {LastName?.Trim()}".Trim();

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? AlternateEmail { get; set; }


    public virtual ICollection<UserDestination> UsersDestinations { get; set; }
        = new HashSet<UserDestination>();

    public virtual ICollection<Destination> FavoriteDestinations { get; set; } 
        = new HashSet<Destination>();
}