using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Horizons.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string? GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}