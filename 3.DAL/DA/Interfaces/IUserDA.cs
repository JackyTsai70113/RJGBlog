using System.Collections.Generic;
using Core;

namespace DAL.DA.Interfaces
{
    public interface IUserDA
    {
        List<User> GetAll();
    }
}