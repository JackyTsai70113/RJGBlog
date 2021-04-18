using System.Collections.Generic;
using Core.Data.Entities;

namespace DAL.DA.Interfaces
{
    public interface IBlogDA
    {
        List<Blog> GetListByUserId(string userId);
    }
}