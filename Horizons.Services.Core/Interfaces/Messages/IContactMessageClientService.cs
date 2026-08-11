using Horizons.Web.ViewModels.Account.Messages;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Horizons.Services.Core.Interfaces.Messages;

public interface IContactMessageClientService
{
    Task SendContactMessageAsync(ContactMessageCreateViewModel model, ClaimsPrincipal userPrincipal);
    Task<List<ContactMessageDetailsViewModel>> GetUserMessagesAsync(string userId);
    Task<ContactMessageDetailsViewModel?> GetMessageDetailsAsync(Guid messageId, string userId);
    Task<int> GetUserUnreadResponsesCountAsync(string userId);
    Task<bool?> MarkAsReadAsync(Guid messageId, string userId);

}
