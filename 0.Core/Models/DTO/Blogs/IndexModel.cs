using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Core.Models.DTO.Blogs
{
    public class IndexModel
    {
        public List<Blog> Blogs { get; set; }

        public int total { get; set; }

        public class Blog
        {
            public int Id { get; set; }
            public string CoverImageUrl { get; set; }
            public string Title { get; set; }
            public string PartialContent { get; set; }
            public string UpdateTime { get; set; }
        }
    }
}