using System.Collections.Generic;
using Core;
using Core.Models.DTO.Views;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        bool CreateUser(RegisterViewModel model);
    }
}