using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Domain.Common
{
    public static class DateTimeConverter
    {
        public static DateTime ShamsiToMiladiTime(string date, string time)
        {
            if (date == null)
            {
                return DateTime.Now;
            }
            else
            {
                var d = date.Replace('-', '/');
                PersianDateTime p = PersianDateTime.Parse(d);
                PersianCalendar calendar = new PersianCalendar();
                var t = time.Split(':');
                int h = 0, m = 0, s = 0;
                h = Convert.ToInt32(t[0]);
                m = Convert.ToInt32(t[1]);
                try { s = Convert.ToInt32(t[2]); } catch (Exception ex) { }
                ;
                DateTime miladiDate = calendar.ToDateTime(p.Year, p.Month, p.Day, h, m, s, 0); ;
                return miladiDate;
            }

        }
        public static string MiladiToShamsi(this DateTime? miladyDate)
        {
            if (miladyDate == null)
            {
                return string.Empty;
            }
            PersianDateTime persianDateTime = new PersianDateTime(miladyDate.Value);
            //var shamsiDate = persianDateTime.ToString();
            //chon faqat tarikh mikham avazesh kardam ke zaman dg bar nagardone
            var shamsiDate = persianDateTime.ToString("yyyy/MM/dd");
            return shamsiDate;
        }
        public static DateTime ShamsiToMiladi(this string? persianDate)
        {
            try
            {
                if (string.IsNullOrEmpty(persianDate))
                    return DateTime.Now;
                var d = persianDate.Replace('-', '/').Split('/');
                if (d.Length == 3)
                {
                    d[1] = d[1].PadLeft(2, '0'); // اضافه کردن صفر به ماه
                    d[2] = d[2].PadLeft(2, '0'); // اضافه کردن صفر به روز
                }
                var formattedDate = string.Join("/", d);
                var pDate = PersianDateTime.Parse(formattedDate);

                return pDate.ToDateTime();
            }
            catch (Exception ex)
            {

                return new DateTime(1900, 01, 01);
            }

        }
        public static DateTime ShamsiToMiladiWithFullDate(this string? persianDate)
        {
            if (string.IsNullOrEmpty(persianDate))
            {
                return DateTime.Now;
            }
            persianDate = persianDate.Substring(0, 10);
            if (string.IsNullOrEmpty(persianDate))
                return DateTime.Now;
            var d = persianDate.Replace('-', '/');
            var dArray = d.Split(' ');
            var pDate = PersianDateTime.Parse(d);
            return pDate.ToDateTime();
        }
        public static string MiladiToShamsiDateHHMM(this DateTime? miladyDate)
        {
            if (miladyDate == null)
            {
                return string.Empty;
            }
            PersianDateTime pdt = new(miladyDate.Value);
            return pdt.ToString("yyyy/MM/dd HH:mm");
        }
        public static string MiladiToShamsiShort(this DateTime? miladiDate)
        {
            try
            {
                if (miladiDate.HasValue)
                {
                    //todo چک شود
                    PersianDateTime persianDate = new PersianDateTime(miladiDate.Value);
                    return persianDate.Date.ToString("yyyy/MM/dd");
                }
                else
                    return "";

            }
            catch
            {
                return "";
            }

        }
        public static DateTime ShamsiToMiladiWithTime(this string? persianDate)
        {

            if (string.IsNullOrEmpty(persianDate))
                return DateTime.Now;
            var d = persianDate.Replace('-', '/');
            var dArray = d.Split(' ');
            var pDate = PersianDateTime.Parse(dArray[0], dArray[1] + ":00");
            return pDate.ToDateTime();
        }
        public static DateTime ShamsiToMiladiTimeCalender(string date, int h, int m, int s)
        {
            if (string.IsNullOrEmpty(date) || string.IsNullOrWhiteSpace(date))
                return DateTime.Now;
            if (date.Length > 0)
            {
                PersianDateTime p = PersianDateTime.Parse(date.Substring(0, 10));
                PersianCalendar calendar = new PersianCalendar();
                DateTime miladiDate = calendar.ToDateTime(p.Year, p.Month, p.Day, h, m, s, 0);  //DateTime.Parse(p.ToString().Substring(0, 10) + " " + time);
                return miladiDate;
            }
            return DateTime.Now;
        }
        public static string MiladiToShamsi(DateTime? miladiDate, ShamsiDateFormat format = ShamsiDateFormat.DateAndHour)
        {
            if (miladiDate.HasValue)
            {
                PersianDateTime persianDate = new PersianDateTime(miladiDate.Value);
                string s = persianDate.ToString();
                if (format == ShamsiDateFormat.Date)
                    return persianDate.ToString(PersianDateTimeFormat.Date);
                if (format == ShamsiDateFormat.DateAndHour)
                    return s.Substring(0, 16);
                if (format == ShamsiDateFormat.Full)
                    return s;
                if (format == ShamsiDateFormat.Time)
                    return s.Substring(11, 5);
                if (format == ShamsiDateFormat.MonthAndHour)
                    return s.Substring(5, 11);
                if (format == ShamsiDateFormat.MonthAndMinutes)
                    return s.Substring(5, 11);
                if (format == ShamsiDateFormat.MonthAndDay)
                    return s.Substring(5, 5);
            }
            return "";
        }
        public static string GetNowPersianDate()
        {
            PersianDateTime now = PersianDateTime.Now;

            string persianDate = now.ToString(PersianDateTimeFormat.Date);

            return persianDate;
        }
        public static int MiladiToShamsiYear(this DateTime miladyDate)
        {
            PersianDateTime persianDateTime = new PersianDateTime(miladyDate);
            return persianDateTime.Year;

        }
        public static int MiladiToShamsiMonth(this DateTime miladyDate)
        {
            PersianDateTime persianDateTime = new PersianDateTime(miladyDate);
            return persianDateTime.Month;

        }

        public enum ShamsiDateFormat
        {
            Full = 0,
            DateAndHour = 1,
            MonthAndHour = 2,
            Date = 3,
            Time = 4,
            MonthAndMinutes = 5,
            MonthAndDay = 6
        }
    }
}
