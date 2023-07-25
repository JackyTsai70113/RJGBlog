using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services.Interfaces;
using Core.Data.Entities;
using Core.Helpers;
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

        public IndexModel GetPagedIndexModel(string userId, int skip, int limit)
        {
            IQueryable<Blog> blogQuery = _blogDA.GetListByUserId(userId);
            List<Blog> dbBlogs = _blogDA.GetPagedEnumerable(blogQuery, skip, limit, out int lastPageIndex).ToList();

            IndexModel model = new()
            {
                Blogs = new List<IndexModel.Blog>(),
                LastPageIndex = lastPageIndex
            };
            foreach (Blog b in dbBlogs)
            {
                string partialContent = b.Content.Length > 30 ? b.Content[..30] + "..." : b.Content;
                model.Blogs.Add(new IndexModel.Blog
                {
                    Id = b.Id.ToString(),
                    CoverImageUrl = b.CoverImageUrl,
                    Title = b.Title,
                    PartialContent = partialContent,
                    UpdateTime = b.UpdateTime.ToLocalTime().ToFullDateShortTime()
                });
            };
            return model;
        }

        public IndexModel GetPagedIndexModel(int skip, int limit)
        {
            IQueryable<Blog> blogQuery = _blogDA.GetList();
            List<Blog> dbBlogs = _blogDA.GetPagedEnumerable(blogQuery, skip, limit, out int lastPageIndex).ToList();

            IndexModel model = new()
            {
                Blogs = new List<IndexModel.Blog>(),
                LastPageIndex = lastPageIndex
            };
            foreach (Blog b in dbBlogs)
            {
                string partialContent = b.Content.Length > 30 ? b.Content[..30] + "..." : b.Content;
                model.Blogs.Add(new IndexModel.Blog
                {
                    Id = b.Id.ToString(),
                    CoverImageUrl = b.CoverImageUrl,
                    Title = b.Title,
                    PartialContent = partialContent,
                    UpdateTime = b.UpdateTime.ToLocalTime().ToFullDateShortTime()
                });
            };
            return model;
        }

        public bool Create(CreateModel model, string userId, out Guid newBlogId)
        {
            bool createResult;
            Blog dbBlog = new()
            {
                CoverImageUrl = model.CoverImageUrl,
                Title = model.Title,
                Content = model.Content,
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow,
                UserId = userId
            };
            createResult = _blogDA.Create(dbBlog);
            newBlogId = dbBlog.Id;
            return createResult;
        }

        public DetailsModel GetDetails(Guid blogId, string userId)
        {
            Blog dbModel = _blogDA.GetById(blogId);

            if (userId != dbModel.UserId)
            {
                throw new Exception("沒有權限");
            }

            string updateTime = dbModel.UpdateTime.ToLocalTime().ToFullDateShortTime();

            DetailsModel model = new()
            {
                Id = dbModel.Id,
                CoverImageUrl = dbModel.CoverImageUrl,
                Title = dbModel.Title,
                Content = dbModel.Content,
                UpdateTime = updateTime
            };
            return model;
        }

        public EditModel GetEditModel(Guid blogId, string userId)
        {
            Blog dbModel = _blogDA.GetById(blogId);

            if (userId != dbModel.UserId)
            {
                throw new Exception("沒有權限");
            }

            EditModel model = new()
            {
                Id = dbModel.Id,
                CoverImageUrl = dbModel.CoverImageUrl,
                Title = dbModel.Title,
                Content = dbModel.Content
            };
            return model;
        }

        public bool Edit(EditModel model, string userId)
        {
            Blog dbBlog = _blogDA.GetById(model.Id);
            dbBlog.CoverImageUrl = model.CoverImageUrl;
            dbBlog.Title = model.Title;
            dbBlog.Content = model.Content;
            dbBlog.UpdateTime = DateTime.UtcNow;
            dbBlog.UserId = userId;

            return _blogDA.SaveChanges() > 0;
        }

        public bool Delete(Guid blogId)
        {
            return _blogDA.Delete(blogId);
        }
    }
}