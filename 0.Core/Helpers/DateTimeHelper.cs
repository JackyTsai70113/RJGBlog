using System;

namespace Core.Helpers {

    /// <summary>
    /// 處理 日期時間 的 Helper
    /// </summary>
    /// <remarks>
    /// 時間日期格式: <see href="https://docs.microsoft.com/zh-tw/dotnet/standard/base-types/standard-date-and-time-format-strings#date-and-time-formats">參考網址</see>
    /// </remarks>
    public static class DateTimeHelper {

        // 完整日期簡短時間, The full date short time
        public static string ToFullDateShortTime(this DateTime dt) {
            return dt.ToString("yyyy年MM月dd日 HH:mm"); // yyyy年MM月dd日 HH:mm
        }
    }
}