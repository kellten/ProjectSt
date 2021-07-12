using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Woom.DataDefine.Util
{
    public class ClsUtilFunc
    {
        public string DateToString(string value)
        {
            ClsUtil clsUtil = new ClsUtil();
            return clsUtil.Mid(value, 1, 8);
        }

        public string DateToString(DateTime value)
        {
            // https://wwwi.tistory.com/327  참조
            return value.ToString("yyyyMMdd");
        }
        public string StringToDate(string value)
        {
            ClsUtil clsUtil = new ClsUtil();
            return clsUtil.Mid(value, 1, 4) + "-" + clsUtil.Mid(value, 1, 4) + "-" + clsUtil.Mid(value, 1, 4);
        }

        public string GetName(Expression<Func<object>> expr)
        {
            var body = ((MemberExpression)expr.Body);
            return body.Member.Name;
        }

        public string DateAddYear(string value, int year)
        {
            return DateToString(ConvertStringToDate(value).AddYears(year));
        }

        public string DateAddMonth(string value, int month)
        {
            return DateToString(ConvertStringToDate(value).AddMonths(month));
        }

        public string DateAddDay(string value, int day)
        {
            return DateToString(ConvertStringToDate(value).AddDays(day));
        }
        /// <summary>
        /// 주의 첫날
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string WeekMonday(string value)
        {
            DateTime dtToday = ConvertStringToDate(value);

            System.Globalization.CultureInfo ciCurrent = System.Threading.Thread.CurrentThread.CurrentCulture;
            DayOfWeek dwFirst = ciCurrent.DateTimeFormat.FirstDayOfWeek;
            DayOfWeek dwToday = ciCurrent.Calendar.GetDayOfWeek(dtToday);

            int iDiff = dwToday - dwFirst;
            DateTime dtFirstDayOfThisWeek = dtToday.AddDays(-iDiff + 1);
            return DateToString(dtFirstDayOfThisWeek);
        }
        /// <summary>
        /// 주의 끝날
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string WeekFriday(string value)
        {
            DateTime dtToday = ConvertStringToDate(value);

            System.Globalization.CultureInfo ciCurrent = System.Threading.Thread.CurrentThread.CurrentCulture;
            DayOfWeek dwFirst = ciCurrent.DateTimeFormat.FirstDayOfWeek;
            DayOfWeek dwToday = ciCurrent.Calendar.GetDayOfWeek(dtToday);

            int iDiff = dwToday - dwFirst;
            DateTime dtFirstDayOfThisWeek = dtToday.AddDays(-iDiff + 1);
            DateTime dtLastDayOfThisWeek = dtFirstDayOfThisWeek.AddDays(4);
            return DateToString(dtLastDayOfThisWeek);
        }
        /// <summary>
        /// 달의 첫날
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string MonthFirstDay(string value)
        {
            DateTime dtToday = ConvertStringToDate(value);
            DateTime firstDay = dtToday.AddDays(1 - dtToday.Day);

            return DateToString(firstDay);
        }

        /// <summary>
        /// 달의 끝날(https://yongtech.tistory.com/53)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string MonthEndDay(string value)
        {
            DateTime dtToday = ConvertStringToDate(value);
            DateTime firstDay = dtToday.AddDays(1 - dtToday.Day);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

            return DateToString(lastDay);
        }

        public DateTime ConvertStringToDate(string value)
        {

            if (value.Length == 8)
            {
                return DateTime.ParseExact(value, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                return DateTime.ParseExact(value, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            }
            // https://yongtech.tistory.com/60 참조

        }
    }
}
