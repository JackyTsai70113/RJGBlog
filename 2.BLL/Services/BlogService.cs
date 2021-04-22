using System;
using System.Collections.Generic;
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

        public IndexModel GetIndexModel(string userId)
        {
            List<Blog> dbBlogs = _blogDA.GetListByUserId(userId);
            IndexModel model = new IndexModel {
                Blogs = new List<IndexModel.Blog>()
            };
            foreach (Blog b in dbBlogs)
            {
                string partialContent = b.Content.Length > 30 ? b.Content.Substring(0, 30) + "..." : b.Content;
                model.Blogs.Add(new IndexModel.Blog {
                    Id = b.Id,
                    CoverImageUrl = b.CoverImageUrl,
                    Title = b.Title,
                    PartialContent = partialContent,
                    UpdateTime = b.UpdateTime.ToLocalTime().ToFullDateShortTime()
                });
            };
            return model;
        }

        public bool Create(CreateModel model, string userId)
        {
            Blog dbBlog = new Blog {
                CoverImageUrl = model.CoverImageUrl,
                Title = model.Title,
                Content = model.Content,
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow,
                UserId = userId
            };
            return _blogDA.Create(dbBlog) > 0;
        }

        public DetailsModel GetDetails(int blogId, string userId)
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

        public EditModel GetEditModel(int blogId, string userId)
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

        public bool Delete(int blogId)
        {
            return _blogDA.Delete(blogId) > 0;
        }
    }
}