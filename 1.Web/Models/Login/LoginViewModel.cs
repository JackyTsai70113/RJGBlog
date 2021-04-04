using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Login
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
        public bool IsRemembered { get; set; }
    }
}