using System;
using System.Collections.Generic;
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
    }
}
