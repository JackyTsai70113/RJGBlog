using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Back.Controllers
{
    //[Authorize(Policy = "BackHome",Roles = "admin")]
    [Authorize("BackHome")]
    [Area("Back")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
