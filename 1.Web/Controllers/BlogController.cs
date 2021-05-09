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

        public IActionResult Index(int skip, int limit)
        {
            string userName = User.Identity.Name;
            if (userName == null)
            {
                return View();
            }
            string userId = _userService.GetUserByNameAsync(userName).Result.Id;
            IndexModel model = _blogService.GetPagedIndexModel(userId, skip, limit);

            _logger.LogInformation("進入我的文章");
            return View(model);
        }

        [HttpGet("/user/{urlUserName}/blogs")]
        public IActionResult Index(string urlUserName, int skip = 0, int limit = 10)
        {
            string userName = User.Identity.Name;
            if (userName != urlUserName)
            {
                _logger.LogInformation($"使用者(userName:{userName}) 嘗試進入 /user/{urlUserName}/blog 頁面。回傳「無法瀏覽」。");
                throw new Exception("無法瀏覽這個頁面，不便之處，敬請見諒。");
            }
            string userId = _userService.GetUserByNameAsync(urlUserName).Result.Id;
            IndexModel model = _blogService.GetPagedIndexModel(userId, skip, limit);
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

            if (ModelState.IsValid)
            {
                _blogService.Create(model, userId, out Guid blogId);
                return RedirectToRoute("Details", new { urlUserName = userName, blogId = blogId });
            } else {
                return View(model);
            }
        }

        [HttpGet("user/{urlUserName}/blog/{blogId}")]
        public IActionResult Details(string urlUserName, Guid blogId)
        {
            string userName = User.Identity.Name;
            if (urlUserName != userName)
            {
                return Unauthorized($"錯誤: 登入者({userName}) 試圖訪問 {urlUserName} 的資訊");
            }
            string userId = _userService.GetUserByNameAsync(userName).Result.Id;

            DetailsModel model = _blogService.GetDetails(blogId, userId);
            return View(model);
        }

        [HttpGet("user/{urlUserName}/blog/{blogId}/edit")]
        public IActionResult Edit(string urlUserName, Guid blogId)
        {
            string userName = User.Identity.Name;
            if (urlUserName != userName)
            {
                return Unauthorized($"錯誤: 登入者({userName}) 試圖訪問 {urlUserName} 的資訊");
            }
            string userId = _userService.GetUserByNameAsync(userName).Result.Id;

            EditModel model = _blogService.GetEditModel(blogId, userId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditModel model)
        {
            string userName = User.Identity.Name;
            string userId = _userService.GetUserByNameAsync(userName).Result.Id;

            if (ModelState.IsValid)
            {
                _blogService.Edit(model, userId);
            }

            return RedirectToAction("Details", new { urlUserName = userName, blogId = model.Id });
        }

        [HttpPost("user/{urlUserName}/blog/{blogId}/delete")]
        public IActionResult Delete(string urlUserName, Guid blogId)
        {
            string userName = User.Identity.Name;
            if (urlUserName != userName)
            {
                return Unauthorized($"錯誤: 登入者({userName}) 試圖訪問 {urlUserName} 的資訊");
            }

            _blogService.Delete(blogId);

            return RedirectToAction("Index", new { urlUserName = userName });
        }
    }
}