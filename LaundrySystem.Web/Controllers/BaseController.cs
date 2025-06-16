using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaundrySystem.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected int CurrentUserId
        {
            get
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userIdString))
                {
                    return 0;
                }

                return int.Parse(userIdString);
            }
        }

        protected string? CurrentUserRole => User.FindFirstValue(ClaimTypes.Role);

        protected string? CurrentUserFullName => User.FindFirstValue("FullName");
    }
}