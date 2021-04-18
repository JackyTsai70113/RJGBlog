using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUserService _userService;
        public RoleService(IUserService userService)
        {
            _userService = userService;
        }

        public Task<bool> AddRoleAsync(IdentityRole role)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddRoleClaimsAsync(IdentityRole role, List<Claim> Claims)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityRole> GetRoleByIdAsync(string Id)
        {
            throw new System.NotImplementedException();
        }
    }
}
