using System;
using System.Globalization;

namespace Core.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/zh-tw/dotnet/standard/base-types/standard-date-and-time-format-strings#date-and-time-formats
    /// </remark>
    public static class DateTimeHelper
    {
        // 完整日期簡短時間, The full date short time
        public static string ToFullDateShortTime(this DateTime dt)
        {
            return dt.ToString("yyyy年MM月dd日 HH:mm"); // yyyy年MM月dd日 HH:mm
        }
    }
}