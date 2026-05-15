using Horizons.Web.ViewModels.Account.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Services.Core.Interfaces.Messages;

public interface ISystemInboxClientService
{
    Task<List<SystemInboxMessageViewModel>> GetUserMessagesAsync(string userId);
    Task<SystemInboxMessageViewModel?> GetMessageDetailsAsync(Guid messageId, string userId);
    Task<int> GetUnreadCountAsync(string userId);
    Task MarkAsReadAsync(Guid messageId, string userId);
}