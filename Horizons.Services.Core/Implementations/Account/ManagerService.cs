using Horizons.Data.Models.Base;
using Horizons.Services.Core.Interfaces.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Services.Core.Implementations.Account;


public class ManagerService : IManagerService
{
    private readonly UserManager<AppUser> _userManager;

    public ManagerService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }


    // Checks if the user with the given ID is in the "Manager" role
    public async Task<bool> IsUserManagerAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }
        if (string.IsNullOrEmpty(userId))
        {
            return false;
        }

        return await _userManager.IsInRoleAsync(user, "Manager");
    }
}