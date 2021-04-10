using System.Collections.Generic;
using Core.Data.Entities;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
    }
}