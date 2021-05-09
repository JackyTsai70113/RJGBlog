using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Entities;
using Core.Domain;
using DAL.DA.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Web.Models.Response;
//using NLog;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUserService _userService;
        private readonly IMenuDA _menuDA;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RoleService> _logger;
        public RoleService(
            IUserService userService,
            IMenuDA menuDA,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            ILogger<RoleService> logger
            )
        {
            _userService = userService;
            _menuDA = menuDA;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IdentityResult> AddRoleAsync(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> AddRoleClaimsAsync(IdentityRole role, List<Claim> Claims)
        {
            IdentityResult result = new IdentityResult();

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

        public async Task<IdentityRole> GetRoleByIdAsync(string Id)
        {
            return await _roleManager.FindByIdAsync(Id);
        }

        public async Task<IdentityRole> GetRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public  List<IdentityRole> GetAllRole()
        {
            return _roleManager.Roles.ToList();
        }

        public List<MenuTree> GetMenuTrees()
        {
            List<MenuTree> menuTrees = new List<MenuTree>();
            List<MenuTree> childMenuTrees = new List<MenuTree>();
            List<Menu> menus = _menuDA.GetList();

            foreach (Menu menu in menus.Where(x => x.ParentId != -1))
            {
                MenuTree menuTree = new MenuTree()
                {
                    Id = menu.Id,
                    Text = menu.Name,
                    ParentId = menu.ParentId
                };

                childMenuTrees.Add(menuTree);
            }

            foreach (Menu menu in menus.Where(x=>x.ParentId == -1))
            {
                MenuTree menuTree = new MenuTree()
                {
                    Id= menu.Id,
                    Text = menu.Name,
                    children = childMenuTrees.Where(x=>x.ParentId == menu.Id).ToList()
                };
                menuTrees.Add(menuTree);
            }
            return menuTrees;
        }

        public List<Menu> GetMenus()
        {
            return _menuDA.GetList();
        }

        public async Task<List<IdentityUser>> GetRoleUsers(string roleName)
        {
            List<IdentityUser> result = new List<IdentityUser>();
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            result = users.ToList();
            return result;
        }

        public async Task<BaseResponse> AddRoleUser(string userName,string roleName)
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
