using Horizons.Web.ViewModels.Account.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Services.Core.Interfaces.Account;


public interface IProfileService
{

    Task<ProfileViewModel?> GetProfileAsync(string userId);







}