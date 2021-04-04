using System.Net;
using Core;
using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using Core.Models.DTO.Account;
using Web.Models.Views;
using Web.Models.Login;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountService _accountService;

        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            LoginAccountModel userModel = new LoginAccountModel
            {
                Account = model.Account,
                Password = model.Password
            };
            bool loginResult = _accountService.Login(userModel);

            BaseResponse response;
            if (loginResult == true)
            {
                response = new BaseResponse(HttpStatusCode.OK, string.Empty, model.Password);
            }
            else
            {
                response = new BaseResponse(HttpStatusCode.Unauthorized, string.Empty, "登入失敗");
            }
            return Json(response);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            RegisterAccountModel userModel = new RegisterAccountModel
            {
                Email = model.Email,
                Password = model.Password
            };
            _accountService.Register(userModel);
            var response = new BaseResponse(HttpStatusCode.Created, string.Empty, model.Password);
            return Json(response);
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}
