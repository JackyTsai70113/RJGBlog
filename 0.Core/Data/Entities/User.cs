using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class User
    {
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
