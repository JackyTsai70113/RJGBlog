using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}