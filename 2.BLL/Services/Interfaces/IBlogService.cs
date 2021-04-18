using Core.Models.DTO.Blogs;

namespace BLL.Services.Interfaces
{
    public interface IBlogService
    {
        IndexModel GetIndexModel(string userId);
        bool Create(CreateModel model);
    }
}