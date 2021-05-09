using System;

namespace Core.Models.DTO.Blogs
{
    public class DetailsModel
    {
        public Guid Id { get; set; }
        public string CoverImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UpdateTime { get; set; }
    }
}