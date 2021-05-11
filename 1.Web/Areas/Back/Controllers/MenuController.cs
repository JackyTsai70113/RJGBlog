using Core.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Web.Areas.Back.Models;
using Web.Services.Interfaces;

namespace Web.Areas.Back.Controllers
{
    [Area("Back")]
    public class MenuController : Controller
    {
        private readonly ILogger<MenuController> _logger;
        private readonly IRoleService _roleService;

        public MenuController(ILogger<MenuController> logger, IRoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        }

        public IActionResult Navigation(string pageArea, string pageController)
        {
            MenuViewModel viewModel = new MenuViewModel
            {
                Menus = GetMenus(),
                PageArea = pageArea,
                PageController = pageController
            };
            return PartialView("_Navigation", viewModel);
        }

        private List<Menu> GetMenus()
        {
            List<Menu> result = _roleService.GetMenus();

            Menu homeMenu = result.Where(x => x.Controller == "Home").FirstOrDefault();
            result.Remove(homeMenu);

            return result;
        }
    }
}
