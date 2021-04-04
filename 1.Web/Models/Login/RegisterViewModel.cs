using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Login
{
    public class RegisterViewModel
    {
        [DisplayName("帳號 *")]
        [Required(ErrorMessage = "此欄位為必填")]
        public string Account { get; set; }

        [DisplayName("密碼 *")]
        [Required(ErrorMessage = "此欄位為必填")]
        public string Password { get; set; }

        [DisplayName("確認密碼 *")]
        [Required(ErrorMessage = "此欄位為必填")]
        [Compare(nameof(Password), ErrorMessage = "密碼需一致")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Email *")]
        [EmailAddress(ErrorMessage = "Email格式不符")]
        [Required(ErrorMessage = "此欄位為必填")]
        public string Email { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "請同意並勾選")]
        public bool IsAgreeWithTerms { get; set; }
    }
}
