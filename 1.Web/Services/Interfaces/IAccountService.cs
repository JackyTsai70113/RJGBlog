using System.Threading.Tasks;

namespace Web.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> GetCurrentUserIdAsync(string userName);
    }
}