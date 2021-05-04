using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Back.Models.Role;
using Web.Models.Response;
using Web.Services.Interfaces;

namespace Web.Areas.Back.Controllers
{
    //[Authorize("RoleHome")]
    [Area("Back")]
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleService _roleService;

        public RoleController(ILogger<RoleController> logger, IRoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            RoleViewModel viewModel = new RoleViewModel();
            viewModel.Roles = _roleService.GetAllRole();
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            RoleEditViewModel viewModel = new RoleEditViewModel();
            if (string.IsNullOrEmpty(id))
            {
                viewModel.ActionType = Core.Enum.ActionType.Create;
            }
            else
            {
                viewModel.Role = await _roleService.GetRoleByIdAsync(id);
                viewModel.ActionType = Core.Enum.ActionType.Edit;
            }

            return View(viewModel);
        }

        public IActionResult GetMenuTreeList()
        {
            var menuTrees = _roleService.GetMenuTrees();
            BaseResponse response = new BaseResponse(System.Net.HttpStatusCode.OK, menuTrees, "取得成功");
            return Json(response);
            //return Json(menuTrees);
        }

        public IActionResult Delete()
        {
            BaseResponse response = new BaseResponse(System.Net.HttpStatusCode.OK, null, "刪除成功");
            return Json(response);
        }
    }
}
