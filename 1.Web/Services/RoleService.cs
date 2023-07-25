using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using Core.Data.Entities;
using Core.Domain;
using DAL.DA.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Web.Areas.Back.Models.Role;
using Web.Models.Response;
//using NLog;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMenuDA _menuDA;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RoleService> _logger;
        public RoleService(
            IMenuDA menuDA,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            ILogger<RoleService> logger
            )
        {
            _menuDA = menuDA;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<BaseResponse> AddRoleAsync(RoleEditViewModel viewModel)
        {
            string msg = string.Empty;
            IdentityRole createRole = new(viewModel.RoleName);
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //建立角色
                    await _roleManager.CreateAsync(createRole);
                    //建立該角色權限
                    List<Menu> menuTrees = _menuDA.GetList();
                    menuTrees = menuTrees.Where(x => viewModel.CheckMenuId.Contains(x.Id)).ToList();
                    foreach (var menuTree in menuTrees)
                    {
                        //後台首頁例外
                        if ((menuTree.Area == "Back" && menuTree.Controller == "Home") || menuTree.ParentId >= 1)
                        {
                            string authValue = menuTree.Area + menuTree.Controller + "AllOK";
                            Claim claim = new(ClaimTypes.Authentication, authValue);
                            IdentityResult claimResult = await _roleManager.AddClaimAsync(createRole, claim);
                        }
                    }
                    //完成交易
                    scope.Complete();
                }
                msg = string.Format("roleName:{0}", viewModel.RoleName);
                return new BaseResponse(System.Net.HttpStatusCode.OK, true, msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new BaseResponse(System.Net.HttpStatusCode.OK, false, ex.Message);
            }
        }

        public async Task<BaseResponse> EditRoleAsync(RoleEditViewModel viewModel)
        {
            string msg = string.Empty;
            try
            {
                //取得該角色
                IdentityRole role = await _roleManager.FindByNameAsync(viewModel.RoleName);
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _roleManager.SetRoleNameAsync(role, viewModel.RoleName);
                    //權限
                }
                return new BaseResponse(System.Net.HttpStatusCode.OK, true, msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new BaseResponse(System.Net.HttpStatusCode.OK, false, ex.Message);
            }
        }

        public async Task<BaseResponse> DeleteRoleAsync(string roleId)
        {
            string msg = string.Empty;
            IdentityResult roleResult = new();
            try
            {
                //取得該角色
                IdentityRole role = await _roleManager.FindByIdAsync(roleId) ?? throw new Exception("查無該角色");
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //取得該角色權限
                    IList<Claim> roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        await _roleManager.RemoveClaimAsync(role, roleClaim);
                    }
                    roleResult = await _roleManager.DeleteAsync(role);
                    //完成交易
                    scope.Complete();
                }
                if (roleResult.Succeeded == false)
                    throw new Exception(roleResult.Errors.FirstOrDefault().Description);

                return new BaseResponse(System.Net.HttpStatusCode.OK, true, msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new BaseResponse(System.Net.HttpStatusCode.OK, false, ex.Message);
            }
        }

        public async Task<IdentityResult> AddRoleClaimsAsync(IdentityRole role, List<Claim> Claims)
        {
            IdentityResult result = new();

            foreach (Claim claim in Claims)
            {
                result = await _roleManager.AddClaimAsync(role, claim);
                if (!result.Succeeded)
                    _logger.LogError("權限儲存失敗，roleName:{0}，Calim:{1}", role.Name, claim.Value);
            }

            return result;
        }

        public async Task<IdentityResult> AddToRoleAsync(IdentityUser user, string roleName)
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task<IdentityRole> GetRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public List<IdentityRole> GetAllRole()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<IList<Claim>> GetClaims(IdentityRole role)
        {
            return await _roleManager.GetClaimsAsync(role);
        }

        public List<MenuTree> GetMenuTrees()
        {
            List<MenuTree> menuTrees = new();
            List<MenuTree> childMenuTrees = new();
            List<Menu> menus = _menuDA.GetList();

            foreach (Menu menu in menus.Where(x => x.ParentId != -1))
            {
                MenuTree menuTree = new()
                {
                    Id = menu.Id,
                    Text = menu.Name,
                    ParentId = menu.ParentId,
                    Sort = menu.Sort,
                    Area = menu.Area,
                    Controller = menu.Controller,
                    Action = menu.Action
                };
                childMenuTrees.Add(menuTree);
            }

            foreach (Menu menu in menus.Where(x => x.ParentId == -1))
            {
                MenuTree menuTree = new()
                {
                    Id = menu.Id,
                    Text = menu.Name,
                    Children = childMenuTrees.Where(x => x.ParentId == menu.Id).OrderBy(x => x.Sort).ToList(),
                    Sort = menu.Sort,
                    Area = menu.Area,
                    Controller = menu.Controller,
                    Action = menu.Action
                };
                menuTrees.Add(menuTree);
            }

            menuTrees = menuTrees.OrderBy(x => x.Sort).ToList();

            return menuTrees;
        }

        public async Task<List<MenuTree>> GetMenuTrees(string roleName)
        {

            List<MenuTree> menuTrees = GetMenuTrees();

            var role = await _roleManager.FindByNameAsync(roleName);
            var claims = await _roleManager.GetClaimsAsync(role);

            foreach (var menuTree in menuTrees)
            {
                //children
                foreach (var childMenu in menuTree.Children)
                {
                    var tree2 = claims.Where(x => x.Value.Contains(childMenu.Area) && x.Value.Contains(childMenu.Controller)).FirstOrDefault();
                    if (tree2 != null)
                        childMenu.Checked = true;
                }

                //parent
                if (string.IsNullOrEmpty(menuTree.Area) && string.IsNullOrEmpty(menuTree.Controller))
                    continue;
                var tree = claims.Where(x => x.Value.Contains(menuTree.Area) && x.Value.Contains(menuTree.Controller)).FirstOrDefault();
                if (tree != null)
                    menuTree.Checked = true;
            }

            return menuTrees;
        }

        public List<Menu> GetMenus()
        {
            return _menuDA.GetList();
        }

        public async Task<List<IdentityUser>> GetRoleUsers(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            List<IdentityUser> result = users.ToList();
            return result;
        }

        public async Task<BaseResponse> AddRoleUser(string userName, string roleName)
        {
            IdentityUser user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return new BaseResponse(System.Net.HttpStatusCode.OK, false, "查無此使用者" + userName);
            }
            else
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                string msg = string.Empty;
                if (result.Errors.FirstOrDefault() != null)
                    msg = result.Errors.FirstOrDefault().Description;
                return new BaseResponse(System.Net.HttpStatusCode.OK, result.Succeeded, msg);
            }
        }

        public async Task<BaseResponse> DeleteRoleUser(string userName, string roleName)
        {
            IdentityUser user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return new BaseResponse(System.Net.HttpStatusCode.OK, false, "查無此使用者" + userName);
            }
            else
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                string msg = string.Empty;
                if (result.Errors.FirstOrDefault() != null)
                    msg = result.Errors.FirstOrDefault().Description;
                return new BaseResponse(System.Net.HttpStatusCode.OK, result.Succeeded, msg);
            }
        }
    }
}
