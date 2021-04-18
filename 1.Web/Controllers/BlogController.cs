using System;
using BLL.Services.Interfaces;
using Core.Models.DTO.Blogs;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly Services.Interfaces.IUserService _userService;
        private readonly IBlogService _blogService;

        public BlogController(Services.Interfaces.IUserService userService, IBlogService blogService)
        {
            _userService = userService;
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            string identityName = User.Identity.Name;
            string userId = _accountService.GetCurrentUserIdAsync(identityName).Result;
            IndexModel model = _blogService.GetIndexModel(userId);
            return View(model);
        }

        [HttpGet("/user/{userName}/blog")]
        public IActionResult Index(string userName)
        {
            string identityName = User.Identity.Name;
            if (identityName != userName)
            {
                throw new Exception("無法瀏覽這個頁面，不便之處，敬請見諒。");
            }
            string userId = _userService.GetUserByNameAsync(userName).Result.Id;
            IndexModel model = _blogService.GetIndexModel(userId);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateModel model)
        {
            string identityName = User.Identity.Name;
            string userId = _accountService.GetCurrentUserIdAsync(identityName).Result;
            model.UserId = userId;
            _blogService.Create(model);
            return View();
        }
    }
}