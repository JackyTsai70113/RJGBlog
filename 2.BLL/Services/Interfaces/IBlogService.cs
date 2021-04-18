using System.Collections.Generic;
using Core.Data.Entities;

namespace BLL.Services.Interfaces
{
    public interface IBlogService
    {
        List<Blog> GetListByUserId(string userId);
    }
}