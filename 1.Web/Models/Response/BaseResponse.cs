using System.Net;
using System.Text.Json.Serialization;

namespace Web.Models.Response
{

    /// <summary>
    /// API 回應基本物件
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="statusCode">HTTP 狀態碼</param>
        /// <param name="data">資料</param>
        /// <param name="message">訊息</param>
        public BaseResponse(HttpStatusCode statusCode, object data, string message)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message;
        }

        /// <summary>
        /// HTTP 狀態碼
        /// </summary>
        /// <value>
        /// HTTP 狀態碼: https://developer.mozilla.org/zh-TW/docs/Web/HTTP/Status
        /// HttpStatusCode 列舉: https://docs.microsoft.com/zh-tw/dotnet/api/system.net.httpstatuscode?view=net-5.0
        /// </value>
        [JsonPropertyName("status")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}