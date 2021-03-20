using System;
using System.Collections.Generic;

namespace Core
{
    public class User
    {
        public int Id { get; set; }
        public string Account { get; set; }
        
        
        public string Name { get; set; }
        public string Email { get; set; }

        public string PassWord { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
