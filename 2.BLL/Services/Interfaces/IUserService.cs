using System.Collections.Generic;
using Core;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();

    }
}