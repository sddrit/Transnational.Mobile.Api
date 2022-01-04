using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Core.Extensions
{
    public static class DateTimeExtension
    {
        public static int DateToInt(this DateTime date)
        {
            return Convert.ToInt32(date.ToString("yyyyMMdd"));

        }
        public static DateTime IntToDate(this int date)
        {
            if (DateTime.TryParseExact(date.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                return dt.ToLocalTime();
            else
                return new DateTime(1900, 01, 01, 0, 0, 0, DateTimeKind.Local);

        }
    }
}
