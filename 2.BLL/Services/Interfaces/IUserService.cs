using System.Collections.Generic;
using Core;
using Core.Models.DTO.User;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        bool Register(RegisterUserModel model);
    }
}