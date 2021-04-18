using System;
using System.Collections.Generic;
using BLL.Services.Interfaces;
using Core.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Models.View.Blog;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IBlogService _blogService;

        public BlogController(IAccountService accountService, IBlogService blogService)
        {
            _accountService = accountService;
            _blogService = blogService;
        }

        [HttpGet("/user/{userName}/blog")]
        public IActionResult Index(string userName)
        {
            string identityName = User.Identity.Name;
            if (identityName != userName)
            {
                throw new Exception("無法瀏覽這個頁面，不便之處，敬請見諒。");
            }
            string userId = _accountService.GetCurrentUserIdAsync(userName).Result;
            List<Blog> blogs = _blogService.GetListByUserId(userId);
            return View(blogs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateModel model)
        {
            return View();
        }
    }
}