using Horizons.Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Horizons.Web.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _homeService;

    public HomeController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _homeService.GetHomePageDataAsync();
        return View(model);
    }
}
