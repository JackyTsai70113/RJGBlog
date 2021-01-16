using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class LoginViewModel
    {
        [DisplayName("帳號")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        public string Password { get; set; }

        [DisplayName("記住我")]
        public bool IsRemeber { get; set; }
    }
}
