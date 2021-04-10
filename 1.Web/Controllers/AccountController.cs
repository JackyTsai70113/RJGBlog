using System.Net;
using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using Core.Models.DTO.Account;
using Web.Helpers;
using Web.Models.Response;
using Web.Models.View.Login;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly JwtHelper _jwtHelper;

        public AccountController(IAccountService accountService, JwtHelper jwtHelper)
        {
            _accountService = accountService;
            _jwtHelper = jwtHelper;
        }

        [HttpGet("~/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
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
                var jwtToken = _jwtHelper.GenerateToken(userModel.Account);
                response = new BaseResponse(HttpStatusCode.OK, jwtToken, "登入成功");
            }
            else
            {
                response = new BaseResponse(HttpStatusCode.Unauthorized, string.Empty, "登入失敗");
            }
            return Json(response);
        }

        [HttpGet("~/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            RegisterAccountModel userModel = new RegisterAccountModel
            {
                Account = model.Account,
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
