using Horizons.Data.Models;
using Horizons.Data.Repositories.Interfaces.CRUD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Data.Repositories.Interfaces.Base;

public interface IFavoriteRepository
    : IFullRepositoryAsync<Favorite, Guid>
{

    Task<Favorite?> GetByCompositeKeyAsync(string userId, Guid productId);

    Task<bool> ExistsAsync(string userId, Guid productId);
}