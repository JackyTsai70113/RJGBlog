using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Data.Entities;
using DAL.DA.Interfaces;
using Microsoft.Extensions.Logging;

namespace DAL.DA
{
    public class BlogDA : BaseDA, IBlogDA
    {
        private readonly ILogger<BlogDA> _logger;
        public BlogDA(ILogger<BlogDA> logger, RJGDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Blog> GetList()
        {
            List<Blog> blogs = _context.Blog.ToList();
            return blogs;
        }

        public List<Blog> GetListByUserId(string userId)
        {
            List<Blog> blogs = _context.Blog.Where(b => b.UserId == userId).ToList();
            return blogs;
        }

        public int Create(Blog blog)
        {
            _context.Blog.Add(blog);
            int changeCount = _context.SaveChanges();
            return changeCount;
        }

        public Blog GetById(string id)
        {
            Blog blog = _context.Blog.Find(id);
            return blog;
        }


        public int Delete(Blog blog)
        {
            _context.Blog.Remove(blog);
            int changeCount = _context.SaveChanges();
            return changeCount;
        }

        public int Delete(List<Blog> blogs)
        {
            _context.Blog.RemoveRange(blogs);
            int changeCount = _context.SaveChanges();
            return changeCount;
        }
    }
}