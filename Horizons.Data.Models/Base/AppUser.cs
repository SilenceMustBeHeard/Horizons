using Horizons.Data.Models.Messages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Horizons.Data.Models.Base;

public class AppUser : IdentityUser
{
    public string FullName => $"{FirstName?.Trim()} {LastName?.Trim()}".Trim();

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? AlternateEmail { get; set; }





    // Navigation properties 
    public virtual ICollection<Favorite> Favorites { get; set; }
        = new HashSet<Favorite>();

    public virtual ICollection<SystemInboxMessage> ReceivedSystemMessages { get; set; }
        = new HashSet<SystemInboxMessage>();

    public virtual ICollection<SystemInboxMessage> SentSystemMessages { get; set; }
        = new HashSet<SystemInboxMessage>();

    public virtual ICollection<ContactMessage> ReceivedContactMessages { get; set; }
        = new HashSet<ContactMessage>();

    public virtual ICollection<ContactMessage> SentContactMessages { get; set; }
        = new HashSet<ContactMessage>();


}