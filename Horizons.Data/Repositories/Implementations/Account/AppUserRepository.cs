using Horizons.Data.Models.Base;
using Horizons.Data.Repositories.Implementations.Base;
using Horizons.Data.Repositories.Interfaces.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Data.Repositories.Implementations.Account;

public class AppUserRepository : RepositoryAsync<AppUser, string>, IAppUserRepository
{
    public AppUserRepository(AppDbContext context) : base(context)
    {
    }

    // Add AppUser-specific methods here if needed, e.g.:
    public async Task<AppUser?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
}