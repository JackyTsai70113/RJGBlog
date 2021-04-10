using System.Collections.Generic;
using Core;

namespace Web.Models.JsonWebToken
{
    public class Token
    {
        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 幾秒過期
        /// </summary>
        public int ExpiresSecond { get; set; }
    }

    public class Payload
    {
        //使用者資訊
        public UserInfo UserInfo { get; set; }
        //過期時間
        public int ExpirationSecond { get; set; }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}