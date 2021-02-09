using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class LoginViewModel
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "此欄位為必填")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "此欄位為必填")]
        public string Password { get; set; }

        [DisplayName("記住我")]
        public bool IsRemeber { get; set; }
    }
}
