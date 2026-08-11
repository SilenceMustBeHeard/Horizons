using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Services.Core.Interfaces.Account;

public interface IManagerService
{
    Task<bool> IsUserManagerAsync(string userId);
}
