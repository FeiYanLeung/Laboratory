using System;

namespace Laboratory
{
    public static class DateTimeExtension
    {
        public static int ToUnixTimestamp(this DateTime utcTime)
        {
            var timeSpan = (utcTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (int)timeSpan.TotalSeconds;
        }

        public static long ToGreenwichTimestamp(this DateTime utcTime)
        {
            var timeSpan = (utcTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (long)timeSpan.TotalMilliseconds;
        }
        /// <summary>
        /// 比较两个时间，并返回时间差
        /// </summary>
        /// <param name="dtUtc"></param>
        /// <param name="targetUtc"></param>
        /// <param name="compareTime">只比较时间</param>
        /// <param name="compareLocal">转换为本地时间后再比较</param>
        /// <returns></returns>
        public static TimeSpan Compare(this DateTime dtUtc, DateTime targetUtc, bool compareTime = false, bool compareLocal = true)
        {
            if (compareLocal)
            {
                dtUtc = dtUtc.ToLocalTime();
                targetUtc = targetUtc.ToLocalTime();
            }

            if (compareTime)
            {
                var _target = DateTime.Parse(String.Format("{0:yyyy/MM/dd} {1:HH:mm:ss}", dtUtc, targetUtc));
                return dtUtc - _target;
            }
            return dtUtc - targetUtc;
        }
    }
}
