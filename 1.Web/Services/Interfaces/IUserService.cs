using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityUser> GetUserByIdAsync(string Id);

        Task<IdentityUser> GetUserByNameAsync(string userName);

        Task<IdentityUser> GetUserByEmailAsync(string Email);

        Task<IdentityResult> CreateUser(string userName, string email, string password);

        Task<string> GetEmailConfirmTokenAsync(string email = null, string userName = null);
    }
}