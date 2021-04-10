using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web.Models.View.Login
{
    public class LoginViewModel
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "此欄位為必填")]
        [JsonPropertyName("account")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "此欄位為必填")]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [DisplayName("記住我")]
        [JsonPropertyName("isRemembered")]
        public bool IsRemembered { get; set; }
    }
}