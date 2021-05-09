using Core.Data.Entities;
using Core.Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models.Response;

namespace Web.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IdentityRole> GetRoleByIdAsync(string Id);

        Task<IdentityRole> GetRoleByNameAsync(string roleName);

        Task<IdentityResult> AddRoleAsync(IdentityRole role);

        Task<IdentityResult> AddRoleClaimsAsync(IdentityRole role, List<Claim> Claims);

        Task<IdentityResult> AddToRoleAsync(IdentityUser user, string roleName);

        List<IdentityRole> GetAllRole();

        List<MenuTree> GetMenuTrees();

        List<Menu> GetMenus();

        Task<List<IdentityUser>> GetRoleUsers(string roleName);

        Task<BaseResponse> AddRoleUser(string userName, string roleName);

        Task<BaseResponse> DeleteRoleUser(string userName, string roleName);
    }
}
