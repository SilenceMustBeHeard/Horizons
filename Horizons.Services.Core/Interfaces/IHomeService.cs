using Horizons.Web.ViewModels.Home;

namespace Horizons.Services.Core.Interfaces;

public interface IHomeService
{
    Task<HomeIndexViewModel> GetHomePageDataAsync();
}
