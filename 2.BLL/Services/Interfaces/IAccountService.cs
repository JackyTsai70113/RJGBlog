using Core.Models.DTO.Account;

namespace BLL.Services.Interfaces
{
    public interface IAccountService
    {
        bool Register(RegisterAccountModel model);
        bool Login(LoginAccountModel model);
    }
}