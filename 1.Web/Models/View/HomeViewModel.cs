using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Views
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Blogs = new List<Blog>();
            Categories = new List<Category>();
        }

        public List<Blog> Blogs { get; set; }

        public List<Category> Categories { get; set; }

        public string SearchKeyWord { get; set; }
    }
}
