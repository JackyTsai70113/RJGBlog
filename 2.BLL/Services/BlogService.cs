using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services.Interfaces;
using Core.Data.Entities;
using Core.Helpers;
using Core.Models.DTO.Blogs;
using DAL.DA.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogDA _blogDA;

        private readonly ILogger<BlogService> _logger;

        public BlogService(IBlogDA blogDA, ILogger<BlogService> logger)
        {
            _blogDA = blogDA;
            _logger = logger;
        }

        public IndexModel GetPagedIndexModel(string userId, int skip, int limit)
        {
            _logger.LogInformation("GetPagedIndexModel");
            
            IQueryable<Blog> blogQuery = _blogDA.GetListByUserId(userId);
            List<Blog> dbBlogs = _blogDA.GetPagedEnumerable(blogQuery, skip, limit, out int lastPageIndex).ToList();

            IndexModel model = new IndexModel {
                Blogs = new List<IndexModel.Blog>(),
                lastPageIndex = lastPageIndex
            };
            foreach (Blog b in dbBlogs)
            {
                string partialContent = b.Content.Length > 30 ? b.Content.Substring(0, 30) + "..." : b.Content;
                model.Blogs.Add(new IndexModel.Blog {
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
            _logger.LogInformation("GetPagedIndexModel");
            
            IQueryable<Blog> blogQuery = _blogDA.GetList();
            List<Blog> dbBlogs = _blogDA.GetPagedEnumerable(blogQuery, skip, limit, out int lastPageIndex).ToList();

            IndexModel model = new IndexModel {
                Blogs = new List<IndexModel.Blog>(),
                lastPageIndex = lastPageIndex
            };
            foreach (Blog b in dbBlogs)
            {
                string partialContent = b.Content.Length > 30 ? b.Content.Substring(0, 30) + "..." : b.Content;
                model.Blogs.Add(new IndexModel.Blog {
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
            bool createResult = false;
            Blog dbBlog = new Blog {
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

            DetailsModel model = new DetailsModel {
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

            EditModel model = new EditModel {
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