﻿using Core.Data.Entities;
using Core.Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Areas.Back.Models.Role;
using Web.Models.Response;

namespace Web.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IdentityRole> GetRoleByIdAsync(string Id);

        Task<IdentityRole> GetRoleByNameAsync(string roleName);

        Task<BaseResponse> AddRoleAsync(RoleEditViewModel viewModel);

        Task<BaseResponse> EditRoleAsync(RoleEditViewModel viewModel);

        Task<IdentityResult> AddRoleClaimsAsync(IdentityRole role, List<Claim> Claims);

        Task<IdentityResult> AddToRoleAsync(IdentityUser user, string roleName);

        List<IdentityRole> GetAllRole();

        Task<IList<Claim>> GetClaims(IdentityRole role);

        List<MenuTree> GetMenuTrees();

        Task<List<MenuTree>> GetMenuTrees(string roleName);

        List<Menu> GetMenus();

        Task<List<IdentityUser>> GetRoleUsers(string roleName);

        Task<BaseResponse> AddRoleUser(string userName, string roleName);

        Task<BaseResponse> DeleteRoleUser(string userName, string roleName);
    }
}
