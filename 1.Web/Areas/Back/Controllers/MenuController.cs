using Core.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Web.Areas.Back.Models;

namespace Web.Areas.Back.Controllers
{
    [Area("Back")]
    public class MenuController : Controller
    {
        public IActionResult Navigation()
        {
            MenuViewModel viewModel = new MenuViewModel
            {
                Menus = GetMenus()
            };
            return PartialView("_Navigation", viewModel);
        }

        private List<Menu> GetMenus()
        {
            List<Menu> result = new List<Menu>();

            Menu menu1 = new Menu()
            {
                Id = 1,
                Name = "權限管理",
                ParentId = -1,
                IsDisable = false,
                Icon = "fas fa-user",
                Sort = 1
            };

            Menu menu2 = new Menu()
            {
                Id = 2,
                Name = "角色權限",
                ParentId = 1,
                IsDisable = false,
                Area = "Back",
                Controller = "RoleController",
                Action = "Index",
                Sort = 2
            };

            Menu menu3 = new Menu()
            {
                Id = 3,
                Name = "帳號管理",
                ParentId = 1,
                IsDisable = false,
                Area = "Back",
                Controller = "AccountController",
                Action = "Index",
                Sort = 1
            };

            result.Add(menu1);
            result.Add(menu2);
            result.Add(menu3);
            return result;
        }
    }
}
