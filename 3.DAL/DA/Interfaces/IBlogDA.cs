using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data.Entities;
using DAL.DA.Base;

namespace DAL.DA.Interfaces
{
    public interface IBlogDA : IBaseDA<Blog>
    {
        IQueryable<Blog> GetList();

        IQueryable<Blog> GetListByUserId(string userId);
        
        bool Create(Blog blog);

        Blog GetById(Guid id);

        bool Delete(Guid blogId);

        int Delete(Blog blog);

        int Delete(List<Blog> blogs);
    }
}