using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Horizons.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected bool IsUserAuthenticated() 
        {
            return User?.Identity != null && User.Identity.IsAuthenticated;
        }

        protected string ? GetUserId() 
        {
            bool isAuthenticated = IsUserAuthenticated();
            if (!isAuthenticated)
            {
                return null;
            }
          

            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
       
    }
}
