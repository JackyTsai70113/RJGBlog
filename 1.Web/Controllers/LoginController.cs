using System.Net;
using Core;
using Microsoft.AspNetCore.Mvc;
using Core.Models.DTO.Views;
using BLL.Services.Interfaces;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            _userService.CreateUser(model);
            var response = new BaseResponse(HttpStatusCode.Created, string.Empty, model.Password);
            return Json(response);
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}
