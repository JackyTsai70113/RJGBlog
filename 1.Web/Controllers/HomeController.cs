using BLL.Services;
using BLL.Services.Interfaces;
using Core;
using DAL.Data;
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
        private readonly RJGDbContext _context;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetUsers();
            HomeViewModel viewModel = new HomeViewModel();
            return View(viewModel);
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
                a = 1234,
                b = "b4567"
            };
            var dd = new BaseResponse(HttpStatusCode.OK, obj, "response OK");
            return Json(dd);
        }

        [HttpGet("GetTagClouds")]
        public JsonResult GetTagClouds()
        {
            List<TagCloud> tagClouds = new List<TagCloud>();
            tagClouds.Add(new TagCloud() { ID = "1", Name = "Lorem" });
            tagClouds.Add(new TagCloud() { ID = "2", Name = "Ipsum" });
            tagClouds.Add(new TagCloud() { ID = "3", Name = "Dolor" });
            tagClouds.Add(new TagCloud() { ID = "4", Name = "Sit" });
            tagClouds.Add(new TagCloud() { ID = "5", Name = "Amet" });
            tagClouds.Add(new TagCloud() { ID = "6", Name = "Consectetur" });
            tagClouds.Add(new TagCloud() { ID = "7", Name = "Adipiscing" });

            var result = new BaseResponse(HttpStatusCode.OK, tagClouds, "response OK");
            return Json(result);
        }
    }
}
