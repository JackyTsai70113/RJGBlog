using System.Collections.Generic;
using Core;
using Core.Models.DTO.Account;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
    }
}