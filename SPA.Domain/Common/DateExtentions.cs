using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Domain.Common
{
    public static class DateExtentions
    {
        public static string ToShamsi(this DateTime datetime)
        {
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(datetime);
            int month = pc.GetMonth(datetime);
            int day = pc.GetDayOfMonth(datetime);

            return $"{year}/{month.ToString("00")}/{day.ToString("00")}";
        }
    }
}
