using Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Web.Models.Response;
using Web.Models.View;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"進入首頁");
            HomeViewModel viewModel = new HomeViewModel();
            return View(viewModel);
        }

        [Authorize("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize("Introduce")]
        public IActionResult Introduce()
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
            List<TagCloud> tagClouds = new List<TagCloud> {
                new TagCloud() { ID = "1", Name = "Lorem" },
                new TagCloud() { ID = "2", Name = "Ipsum" },
                new TagCloud() { ID = "3", Name = "Dolor" },
                new TagCloud() { ID = "4", Name = "Sit" },
                new TagCloud() { ID = "5", Name = "Amet" },
                new TagCloud() { ID = "6", Name = "Consectetur" },
                new TagCloud() { ID = "7", Name = "Adipiscing" }
            };

            var result = new BaseResponse(HttpStatusCode.OK, tagClouds, "response OK");
            return Json(result);
        }
    }
}