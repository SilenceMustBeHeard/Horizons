using Horizons.Data.Models;
using Horizons.Data.Repositories.Implementations.Base;
using Horizons.Data.Repositories.Interfaces.Interactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Data.Repositories.Implementations.Implementations;

public class FavoriteRepository :
         RepositoryAsync<Favorite, Guid>,
         IFavoriteRepository
{
    public FavoriteRepository(AppDbContext context)
        : base(context)
    {
    }


    // retrieves a favorite by its composite key (userId and productId)

    public async Task<Favorite?> GetByCompositeKeyAsync(string userId, Guid destinationId)


        => await _dbSet
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(f =>
                f.UserId == userId &&
               f.DestinationId == destinationId);



    // checks if a favorite exists for a given user and product
    // excluding deleted favorites

    public async Task<bool> ExistsAsync(string userId, Guid destinationId)
        => await _dbSet
            .AnyAsync(f =>
                f.UserId == userId &&
                f.DestinationId == destinationId &&
                !f.IsDeleted);
}