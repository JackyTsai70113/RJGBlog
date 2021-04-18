using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IdentityRole> GetRoleByIdAsync(string Id);

        Task<bool> AddRoleAsync(IdentityRole role);

        Task<bool> AddRoleClaimsAsync(IdentityRole role, List<Claim> Claims);
    }
}
