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
        private readonly IAccountService _accountService;
        private readonly IBlogService _blogService;

        public BlogController(ILogger<BlogController> logger, IAccountService accountService, IBlogService blogService)
        {
            _logger = logger;
            _accountService = accountService;
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            string identityName = User.Identity.Name;
            if (identityName == null)
            {
                return View();
            }
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
                _logger.LogInformation($"使用者(userName:{identityName}) 嘗試進入 /user/{userName}/blog 頁面。回傳「無法瀏覽」。");
                throw new Exception("無法瀏覽這個頁面，不便之處，敬請見諒。");
            }
            string userId = _accountService.GetCurrentUserIdAsync(userName).Result;
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