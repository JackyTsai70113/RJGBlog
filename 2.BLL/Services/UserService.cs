using System.Collections.Generic;
using Core;
using DAL.DA.Interfaces;

namespace BLL.Services
{
    public class UserService
    {
        public List<User> GetUsers()
        {
            UserDA da = new UserDA();
            var users = da.GetAll();
            return users;
        }
    }
}