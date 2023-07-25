using System;
using Core.Models.DTO.Blogs;

namespace BLL.Services.Interfaces
{
    public interface IBlogService
    {
        IndexModel GetPagedIndexModel(string userId, int skip, int limit);

        IndexModel GetPagedIndexModel(int skip, int limit);

        bool Create(CreateModel model, string userId, out Guid newBlogId);

        DetailsModel GetDetails(Guid blogId, string userId);

        EditModel GetEditModel(Guid blogId, string userId);

        bool Edit(EditModel model, string userId);

        bool Delete(Guid blogId);
    }
}