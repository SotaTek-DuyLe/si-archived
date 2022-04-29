using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace si_automated_tests.Source.Core
{
    public class CommonUtil
    {
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[(new Random()).Next(s.Length)]).ToArray());
        }
        public static string GetRandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[(new Random()).Next(s.Length)]).ToArray());
        }
        public static string GetLocalTimeNow(string format)
        {
            return DateTime.Now.ToString(format).Replace('-', '/');
        }
        public static DateTime GetLocalTimeNow()
        {
            return DateTime.Now;
        }
        public static string GetLocalTimeMinusDay(string format, int day)
        {
            return DateTime.Now.AddDays(day).ToString(format).Replace('-', '/');
        }
        public static string GetLocalTimeMinusMonth(string format, int month)
        {
            return DateTime.Now.AddMonths(month).ToString(format).Replace('-', '/');
        }
        public static string GetLocalTimeFromDate(string now, string format, int day)
        {
            return DateTime.ParseExact(now, format, CultureInfo.InvariantCulture).AddDays(day).ToString(format).Replace('-', '/');
        }

        public static string GetUtcTimeMinusDay(string format, int day)
        {
            return DateTime.UtcNow.AddDays(day).ToString(format).Replace('-', '/');
        }

        public static string GetUtcTimeNow(string format)
        {
            return DateTime.UtcNow.ToString(format).Replace('-', '/');
        }
        public static string GetTimeMinusHour(string time, string format, int hour)
        {
            return DateTime.ParseExact(time, format, CultureInfo.InvariantCulture).AddHours(hour).ToString(format).Replace('-', '/');
        }
        public static string ParseDateTimeToFormat(DateTime date, string format)
        {
            return date.ToString(format);
        }

        public static string GetLocalDayMinusDay(int day)
        {
            return DateTime.Now.AddDays(day).Day.ToString();
        }
        
        public int GetRandomNumberBetweenRange(int min, int max)
        {
            Random rnd = new Random();
            int num = rnd.Next(min, max - 1);
            return num;
        }
    }
}
