using Microsoft.AspNetCore.Mvc;
using Core.Models.DTO.Views;
using BLL.Services.Interfaces;
using Core;
using System;
using System.Net;

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