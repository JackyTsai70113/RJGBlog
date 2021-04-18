using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AccountService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetCurrentUserIdAsync(string userName)
        {
            IdentityUser user = await _userManager.FindByNameAsync(userName);
            string userId = user.Id;
            return userId;
        }
    }
}