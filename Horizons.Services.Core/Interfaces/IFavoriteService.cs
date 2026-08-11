using Horizons.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Services.Core.Interfaces;


public interface IFavoriteService
{


    Task<IEnumerable<BaseDestinationViewModel>> GetUserFavoritesAsync(string userId);
    Task<bool> ToggleFavoriteAsync(string userId, Guid destinationId);
    Task<bool> IsFavoriteAsync(string userId, Guid destinationId);
}
