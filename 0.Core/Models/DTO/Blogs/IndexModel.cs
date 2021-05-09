using System;
using System.Collections.Generic;

namespace Core.Models.DTO.Blogs
{
    public class IndexModel
    {
        public List<Blog> Blogs { get; set; }

        public int lastPageIndex { get; set; }

        public class Blog
        {
            public string Id { get; set; }
            public string CoverImageUrl { get; set; }
            public string Title { get; set; }
            public string PartialContent { get; set; }
            public string UpdateTime { get; set; }
        }
    }
}