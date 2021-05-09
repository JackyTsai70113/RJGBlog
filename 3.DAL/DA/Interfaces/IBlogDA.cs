using System.Collections.Generic;
using System.Linq;
using Core.Data.Entities;
using DAL.DA.Base;

namespace DAL.DA.Interfaces
{
    public interface IBlogDA : IBaseDA
    {
        IQueryable<Blog> GetList();

        IQueryable<Blog> GetListByUserId(string userId);

        List<Blog> GetPagedList(int skip, int limit, out int total);
        List<Blog> GetPagedListByUserId(string userId, int skip, int limit, out int total);
        int Create(Blog blog);

        Blog GetById(int id);

        int Delete(int blogId);

        int Delete(Blog blog);

        int Delete(List<Blog> blogs);
    }
}