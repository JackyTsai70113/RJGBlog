using System.Collections.Generic;
using Core;

namespace DAL.DA.Interfaces
{
    public interface IUserDA
    {
        List<User> GetAll();
        void Create(User user);
        User GetByAccount(string account);

        User GetByEmail(string email);
    }
}