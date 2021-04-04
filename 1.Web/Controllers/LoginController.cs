using System.Net;
using Core;
using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using Core.Models.DTO.User;
using Web.Models.Views;
using Web.Models.Login;

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
            RegisterUserModel userModel = new RegisterUserModel
            {
                Email = model.Email,
                Password = model.Password
            };
            _userService.Register(userModel);
            var response = new BaseResponse(HttpStatusCode.Created, string.Empty, model.Password);
            return Json(response);
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}
