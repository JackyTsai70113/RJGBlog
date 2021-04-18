using System.Collections.Generic;
using Core.Data.Entities;

namespace DAL.DA.Interfaces
{
    public interface IBlogDA
    {
        List<Blog> GetList();

        List<Blog> GetListByUserId(string userId);

        int Create(Blog blog);

        Blog GetById(string id);


        int Delete(Blog blog);

        int Delete(List<Blog> blogs);
    }
}