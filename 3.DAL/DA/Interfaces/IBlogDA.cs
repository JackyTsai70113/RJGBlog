using System.Collections.Generic;
using Core.Data.Entities;
using DAL.DA.Base;

namespace DAL.DA.Interfaces
{
    public interface IBlogDA : IBaseDA
    {
        List<Blog> GetList();

        List<Blog> GetListByUserId(string userId);

        int Create(Blog blog);

        Blog GetById(int id);

        int Delete(int blogId);

        int Delete(Blog blog);

        int Delete(List<Blog> blogs);
    }
}