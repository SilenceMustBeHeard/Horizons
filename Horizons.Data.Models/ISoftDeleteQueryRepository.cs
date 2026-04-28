using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Data.Models;

public interface ISoftDeleteQueryRepository<TEntity, TKey> where TEntity : class
{
    IQueryable<TEntity> GetAllIncludingDeleted();
    Task<bool> ToggleStatusAsync(TEntity entity);
}