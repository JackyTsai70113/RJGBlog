using System;
using BLL.Services.Interfaces;
using Core.Models.DTO.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly IUserService _userService;
        private readonly IBlogService _blogService;

        public BlogController(ILogger<BlogController> logger, IUserService userService, IBlogService blogService)
        {
            _logger = logger;
            _userService = userService;
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            string userName = User.Identity.Name;
            if (userName == null)
            {
                return View();
            }
            string userId = _userService.GetUserByNameAsync(userName).Result.Id;
            IndexModel model = _blogService.GetIndexModel(userId);
            return View(model);
        }

        [HttpGet("/user/{urlUserName}/blog")]
        public IActionResult Index(string urlUserName)
        {
            string userName = User.Identity.Name;
            if (userName != urlUserName)
            {
                _logger.LogInformation($"使用者(userName:{userName}) 嘗試進入 /user/{urlUserName}/blog 頁面。回傳「無法瀏覽」。");
                throw new Exception("無法瀏覽這個頁面，不便之處，敬請見諒。");
            }
            string userId = _userService.GetUserByNameAsync(urlUserName).Result.Id;
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
            string userName = User.Identity.Name;
            string userId = _userService.GetUserByNameAsync(userName).Result.Id;
            model.UserId = userId;
            _blogService.Create(model);
            return View();
        }
    }
}