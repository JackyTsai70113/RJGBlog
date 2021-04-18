using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Domain
{
    public class Blog
    {
        [DisplayName("封面")]
        public string Cover { get; set; }

        [DisplayName("作者")]
        public string Author { get; set; }

        [DisplayName("產出日期")]
        public DateTime CreateDate { get; set; }

        [DisplayName("留言數")]
        public int Comments { get; set; }

        [DisplayName("標題")]
        public string Title { get; set; }

        [DisplayName("部分內文")]
        public string PartialContent { get; set; }
    }
}
