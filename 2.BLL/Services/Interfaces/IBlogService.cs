using Core.Data.Entities;
using Core.Models.DTO.Blogs;

namespace BLL.Services.Interfaces
{
    public interface IBlogService
    {
        IndexModel GetPagedIndexModel(string userId, int skip, int limit);

        bool Create(CreateModel model, string userId);

        DetailsModel GetDetails(int blogId, string userId);

        EditModel GetEditModel(int blogId, string userId);

        bool Edit(EditModel model, string userId);

        bool Delete(int blogId);
    }
}