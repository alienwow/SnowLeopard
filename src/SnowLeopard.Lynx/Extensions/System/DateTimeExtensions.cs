using System;
using System.Collections.Generic;
using SnowLeopard.Lynx.Enums;

namespace SnowLeopard.Lynx.Extensions
{
    /// <summary>
    /// DateTimeExtensions
    /// </summary>
    public static class DateTimeExtensions
    {
        #region UnixTimestamp

        /// <summary>
        /// UTC 1970/01/01 00:00:00
        /// </summary>
        public static DateTime Jan1st1970 { get; } = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public const long JAN_1_1970_TICKS = 621355968000000000;

        /// <summary>
        /// ToUnixTimestamp
        /// </summary>
        /// <param name="self"></param>
        /// <returns>返回距离1970-01-01 00:00:00经过的毫秒数</returns>
        public static long ToUnixTimestamp(this DateTime self)
        {
            return (self.ToUniversalTime().Ticks - JAN_1_1970_TICKS) / 10000;
        }

        /// <summary>
        /// 根据时间戳获取 UTC 时间
        /// </summary>
        /// <param name="self"></param>
        /// <returns>返回UTC时间</returns>
        public static DateTime ToUtcTime(this long self)
        {
            return Jan1st1970.AddMilliseconds(self);
        }

        /// <summary>
        /// 根据时间戳获取 LocalTime
        /// </summary>
        /// <param name="self"></param>
        /// <returns>返回本地时间</returns>
        public static DateTime ToLocalTime(this long self)
        {
            return Jan1st1970.AddMilliseconds(self).ToLocalTime();
        }

        #endregion

        #region DateTime Helper

        /// <summary>
        /// WeekOfYear
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime self)
        {
            var startDayOfYear = new DateTime(self.Year, 1, 1);
            var weekOffset = 7 - (startDayOfYear.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)startDayOfYear.DayOfWeek) + 1;
            var weekOfYear = (int)Math.Ceiling((self.DayOfYear - weekOffset) / 7.0 + (weekOffset == 0 ? 0 : 1));

            return weekOfYear;
        }

        /// <summary>
        /// 获取 指定的一周所在年份 的开始及结束时间
        /// </summary>
        /// <param name="yearNum">所在年份</param>
        /// <param name="weekOfYear">周数</param>
        /// <param name="startDay">指定周开始时间</param>
        /// <param name="endDay">指定周结束时间</param>
        public static void WeekDays(int yearNum, int weekOfYear, out DateTime startDay, out DateTime endDay)
        {
            var startDayOfYear = new DateTime(yearNum, 1, 1, 0, 0, 0);

            var weekOffset = 7 - (startDayOfYear.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)startDayOfYear.DayOfWeek) + 1;
            startDay = startDayOfYear.AddDays(7 * (weekOfYear - (weekOffset == 0 ? 0 : 1)) + weekOffset - 7);
            endDay = startDay.AddDays(7);
        }

        #endregion

        #region 星期相关

        /// <summary>
        /// 获取周几
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DayOfWeekEnum DayIndexOfWeek(this DateTime self)
        {
            if (self.DayOfWeek == DayOfWeek.Sunday)
            {
                return DayOfWeekEnum.Sunday;
            }
            else
            {
                return (DayOfWeekEnum)self.DayOfWeek;
            }
        }

        #region 中文星期

        private static readonly Dictionary<int, string> _dayIndexOfWeekDic = new Dictionary<int, string>
        {
            { 0,"日"},
            { 1,"一"},
            { 2,"二"},
            { 3,"三"},
            { 4,"四"},
            { 5,"五"},
            { 6,"六"},
            { 7,"日"}
        };

        /// <summary>
        /// 获取星期的中文数字
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string DayIndexOfWeekCN(this DateTime self)
        {
            return DayIndexOfWeekCN(self.DayOfWeek);
        }

        /// <summary>
        /// 获取星期的中文数字
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string DayIndexOfWeekCN(int index)
        {
            if (index < 0 || index > 7)
                throw new Exception("非法的星期");
            return _dayIndexOfWeekDic[index];
        }

        /// <summary>
        /// 获取星期的中文数字
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static string DayIndexOfWeekCN(DayOfWeekEnum dayOfWeek)
        {
            return _dayIndexOfWeekDic[(int)dayOfWeek];
        }

        /// <summary>
        /// 获取星期的中文数字
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static string DayIndexOfWeekCN(DayOfWeek dayOfWeek)
        {
            return _dayIndexOfWeekDic[(int)dayOfWeek];
        }

        #endregion

        /// <summary>
        /// 获取当前周第一天
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfCurrentWeek(this DateTime self)
        {
            var dayOfWeek = (int)self.DayOfWeek;
            var diff = (dayOfWeek == 0 ? 7 : dayOfWeek) - 1;
            var first = self.AddDays(-1 * diff).ToString("yyyy-MM-dd");
            return DateTime.Parse(first);
        }

        /// <summary>
        /// 获取当前周最后天
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime LastDayOfCurrentWeek(this DateTime self)
        {
            var dayOfWeek = (int)self.DayOfWeek;
            var diff = dayOfWeek == 0 ? 0 : (7 - dayOfWeek);
            var first = self.AddDays(diff).ToString("yyyy-MM-dd");
            return DateTime.Parse(first);
        }

        /// <summary>
        /// 获取当前周时间列表
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IList<DateTime> GetWeekList(this DateTime self)
        {
            var dayOfWeek = (int)self.DayOfWeek;

            var first = self.FirstDayOfCurrentWeek();
            var list = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                list.Add(first.AddDays(i));
            }
            return list;
        }

        /// <summary>
        /// 获取当前时间所在周的开始及结束时间
        /// </summary>
        /// <param name="self"></param>
        /// <param name="startDay">指定周开始时间</param>
        /// <param name="endDay">指定周结束时间</param>
        public static void WeekDays(this DateTime self, out DateTime startDay, out DateTime endDay)
        {
            WeekDays(self.Year, self.WeekOfYear(), out startDay, out endDay);
        }

        #endregion
    }
}
