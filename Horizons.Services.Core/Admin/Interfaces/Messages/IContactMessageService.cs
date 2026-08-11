using Horizons.Web.ViewModels.Account.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Services.Core.Admin.Interfaces.Messages;

public interface IContactMessageService
{
    Task<List<ContactMessageDetailsViewModel>> GetAdminMessagesAsync(string adminId);
    Task<ContactMessageDetailsViewModel?> GetMessageDetailsAsync(Guid messageId, string adminId);
    Task RespondToMessageAsync(Guid messageId, string response, string adminId);
    Task MarkMessageAsReadAsync(Guid messageId, string adminId);
    Task<int> GetUnreadCountAsync(string adminId);
    Task MarkAllMessagesAsReadAsync(string adminId);
}
