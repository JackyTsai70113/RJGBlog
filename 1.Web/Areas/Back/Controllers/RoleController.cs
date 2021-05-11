using Core.Domain;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize("BackRole")]
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

        public IActionResult RoleAccount(string roleName)
        {
            RoleAccountViewModel viewModel = new RoleAccountViewModel();
            viewModel.RoleName = roleName;

            return View(viewModel);
        }

        //Api

        /// <summary>取得功能樹</summary>
        [HttpGet]
        public async Task<IActionResult> GetMenuTreeList(string roleName = null)
        {
            List<MenuTree> menuTrees = new List<MenuTree>();
            
            if (!string.IsNullOrEmpty(roleName))
                menuTrees = await _roleService.GetMenuTrees(roleName);
            else
                menuTrees = _roleService.GetMenuTrees();

            BaseResponse response = new BaseResponse(System.Net.HttpStatusCode.OK, menuTrees, "取得成功");
            return Json(response);
        }

        /// <summary>取得歸屬該角色的帳號</summary>
        [HttpGet]
        public async Task<IActionResult> GetRoleUsers(string roleName)
        {
            RoleAccountViewModel viewModel = new RoleAccountViewModel();
            viewModel.Users = await _roleService.GetRoleUsers(roleName);
            return PartialView("_roleAccountList", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAdd(RoleEditViewModel viewModel)
        {
            BaseResponse response = null;
            if (viewModel.ActionType == Core.Enum.ActionType.Create)
                response = await _roleService.AddRoleAsync(viewModel);
            else
                response = await _roleService.EditRoleAsync(viewModel);
            return Json(response);
        }

        /// <summary>新增帳號</summary>
        [HttpPost]
        public async Task<IActionResult> RoleAccountAdd(string userName, string roleName)
        {
            return Json(await _roleService.AddRoleUser(userName, roleName));
        }

        /// <summary>刪除角色</summary>
        [HttpDelete]
        public IActionResult RoleDelete(string roleId)
        {
            BaseResponse response = new BaseResponse(System.Net.HttpStatusCode.OK, null, "刪除成功");
            return Json(response);
        }

        /// <summary>刪除帳號</summary>
        [HttpDelete]
        public async Task<IActionResult> RoleAccountDelete(string userName, string roleName)
        {
            return Json(await _roleService.DeleteRoleUser(userName, roleName));
        }


    }
}
