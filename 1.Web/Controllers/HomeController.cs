﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using BLL.Services.Interfaces;
using Core.Domain;
using Core.Models.DTO.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models.Response;
using Web.Models.View;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IBlogService _blogService;

        public HomeController(ILogger<HomeController> logger, IBlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"進入首頁");
            HomeViewModel viewModel = new();
            return View(viewModel);
        }

        public IActionResult Index2(int skip = 0, int limit = 10)
        {
            IndexModel model = _blogService.GetPagedIndexModel(skip, limit);

            _logger.LogInformation("進入我的文章");
            return View(model);
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
            List<TagCloud> tagClouds = new()
            {
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