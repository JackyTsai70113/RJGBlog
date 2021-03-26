using System;
using System.Net;

//using Newtonsoft.Json;

namespace Core {

    /// <summary>
    /// API 回應基本物件
    /// </summary>
    public class BaseResponse {

        public BaseResponse(HttpStatusCode statusCode, object data, string message) {
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
        //[JsonProperty(PropertyName = "status")]
        public HttpStatusCode StatusCode { get; set; }

        //[JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

        //[JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}