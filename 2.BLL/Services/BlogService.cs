using System.Collections.Generic;
using BLL.Services.Interfaces;
using Core.Data.Entities;
using DAL.DA.Interfaces;

namespace BLL.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogDA _blogDA;

        public BlogService(IBlogDA blogDA)
        {
            _blogDA = blogDA;
        }

        public List<Blog> GetListByUserId(string userId)
        {
            List<Blog> blogs = _blogDA.GetListByUserId(userId);
            return blogs;
        }
    }
}