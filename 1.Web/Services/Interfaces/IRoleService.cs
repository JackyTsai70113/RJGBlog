using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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
    }
}
