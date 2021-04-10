﻿using Core;
using System.Collections.Generic;

namespace Web.Models.View
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
