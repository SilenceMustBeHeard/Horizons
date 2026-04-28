namespace Horizons.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using ViewModels;

    public class HomeController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {

            try
            {
                if (User?.Identity != null && User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Destination");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
