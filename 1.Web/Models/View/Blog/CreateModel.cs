using System.ComponentModel.DataAnnotations;

namespace Web.Models.View.Blog
{
    public class CreateModel
    {
        [Display(Name = "封面圖片網址")]
        [Required(ErrorMessage = "此欄位為必填")]
        [StringLength(255, ErrorMessage = "The {0} value cannot exceed {1}")]
        public string CoverImageUrl { get; set; }

        [Display(Name = "標題")]
        [Required(ErrorMessage = "此欄位為必填")]
        [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1}")]
        public string Title { get; set; }

        [Display(Name = "內文")]
        [Required(ErrorMessage = "此欄位為必填")]
        [StringLength(4000, ErrorMessage = "The {0} value cannot exceed {1}")]
        public string Content { get; set; }
    }
}