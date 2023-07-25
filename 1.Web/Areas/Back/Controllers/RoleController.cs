using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Back.Models.Role;
using Web.Models.Response;
using Web.Services.Interfaces;

namespace Web.Areas.Back.Controllers
{
    [Authorize("BackRole")]
    [Area("Back")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            RoleViewModel viewModel = new()
            {
                Roles = _roleService.GetAllRole()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            RoleEditViewModel viewModel = new();
            if (string.IsNullOrEmpty(id))
            {
                viewModel.ActionType = Core.Enum.ActionType.Create;
            }
            else
            {
                IdentityRole role = await _roleService.GetRoleByIdAsync(id);
                viewModel.RoleName = role.Name;
                viewModel.ActionType = Core.Enum.ActionType.Edit;
            }

            return View(viewModel);
        }

        public IActionResult RoleAccount(string roleName)
        {
            RoleAccountViewModel viewModel = new()
            {
                RoleName = roleName
            };

            return View(viewModel);
        }

        //Api

        /// <summary>取得功能樹</summary>
        [HttpGet]
        public async Task<IActionResult> GetMenuTreeList(string roleName = null)
        {
            List<MenuTree> menuTrees;

            if (!string.IsNullOrEmpty(roleName))
                menuTrees = await _roleService.GetMenuTrees(roleName);
            else
                menuTrees = _roleService.GetMenuTrees();

            BaseResponse response = new(System.Net.HttpStatusCode.OK, menuTrees, "取得成功");
            return Json(response);
        }

        /// <summary>取得歸屬該角色的帳號</summary>
        [HttpGet]
        public async Task<IActionResult> GetRoleUsers(string roleName)
        {
            RoleAccountViewModel viewModel = new()
            {
                Users = await _roleService.GetRoleUsers(roleName)
            };
            return PartialView("_roleAccountList", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAdd(RoleEditViewModel viewModel)
        {
            BaseResponse response = null;
            switch (viewModel.ActionType)
            {
                case Core.Enum.ActionType.Create:
                    response = await _roleService.AddRoleAsync(viewModel);
                    break;
                case Core.Enum.ActionType.Edit:
                    response = await _roleService.EditRoleAsync(viewModel);
                    break;
                case Core.Enum.ActionType.Delete:
                    break;
                default:
                    response = new BaseResponse(System.Net.HttpStatusCode.OK, false, "ActionType錯誤");
                    break;
            }
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
        public async Task<IActionResult> RoleDelete(string roleId)
        {
            BaseResponse response = await _roleService.DeleteRoleAsync(roleId);
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
