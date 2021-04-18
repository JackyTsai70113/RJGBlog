using System;
using System.Collections.Generic;
using BLL.Services.Interfaces;
using Core.Data.Entities;
using Core.Models.DTO.Blogs;
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

        public IndexModel GetIndexModel(string userId)
        {
            List<Blog> dbBlogs = _blogDA.GetListByUserId(userId);
            IndexModel model = new IndexModel
            {
                Blogs = new List<IndexModel.Blog>()
            };
            foreach (Blog b in dbBlogs)
            {
                string partialContent = b.Content.Substring(0, 30);
                model.Blogs.Add(new IndexModel.Blog
                {
                    Id = b.Id,
                    CoverImageUrl = b.CoverImageUrl,
                    Title = b.Title,
                    PartialContent = partialContent,
                    UpdateTime = b.UpdateTime,
                    Author = "作者欄位"
                });
            };
            return model;
        }

        public bool Create(CreateModel model)
        {
            Blog dbBlog = new Blog
            {
                CoverImageUrl = model.CoverImageUrl,
                Title = model.Title,
                Content = model.Content,
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow,
                UserId = model.UserId
            };
            return _blogDA.Create(dbBlog) > 0;
        }
    }
}