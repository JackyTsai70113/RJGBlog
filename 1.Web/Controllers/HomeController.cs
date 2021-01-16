using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly RJGDbContext _context;

        //public HomeController(ILogger<HomeController> logger, RJGDbContext context)
        //{
        //    _logger = logger;
        //    _context = context;
        //}

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("Test")]
        public JsonResult Test()
        {
            var obj = new
            {
                a = "a1234",
                b = "b4567"
            };
            var dd = new BaseResponse(HttpStatusCode.OK, obj, "response OK");
            return Json(dd);
        }
    }
}
