﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
