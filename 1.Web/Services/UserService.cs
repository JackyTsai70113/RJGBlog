using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityUser> GetUserByNameAsync(string userName)
        {
            IdentityUser user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<IdentityUser> GetUserByEmailAsync(string Email)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(Email);
            return user;
        }

        public async Task<IdentityResult> CreateUser(string userName, string email, string password)
        {
            IdentityUser user = new() { UserName = userName, Email = email };
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<string> GetEmailConfirmTokenAsync(string email = null, string userName = null)
        {
            string result = string.Empty;
            IdentityUser user = new();
            if (!string.IsNullOrEmpty(email))
                user = await GetUserByEmailAsync(email);
            if (!string.IsNullOrEmpty(userName))
                user = await GetUserByNameAsync(userName);

            if (user != null)
            {
                string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                result = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            }
            return result;
        }
    }
}