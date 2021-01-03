using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Back.Controllers
{
    public class LoginController : Controller
    {
        [Area("Back")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
