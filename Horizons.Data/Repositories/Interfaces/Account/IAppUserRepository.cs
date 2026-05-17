using Horizons.Data.Models.Base;
using Horizons.Data.Repositories.Interfaces.CRUD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Data.Repositories.Interfaces.Account;

public interface IAppUserRepository
     : IFullRepositoryAsync<AppUser, string>
{
}