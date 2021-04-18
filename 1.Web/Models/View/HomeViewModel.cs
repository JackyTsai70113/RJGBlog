using Core.Domain;
using System.Collections.Generic;

namespace Web.Models.View
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Blogs = new List<Core.Domain.Blog>();
            Categories = new List<Category>();
        }

        public List<Core.Domain.Blog> Blogs { get; set; }

        public List<Category> Categories { get; set; }

        public string SearchKeyWord { get; set; }
    }
}
