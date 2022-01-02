namespace TransnationalLanka.Rms.Mobile.Services.Util.DateTimeUtil
{
    public static class Converter
    {
        public static DateTime ConvertToLK(this DateTime dateTime)
        {
            return  dateTime.AddHours(5).AddMinutes(30);
        }

        public static DateTime ConvertToUtc(this DateTime dateTime)
        {
            return dateTime.AddHours(-5).AddMinutes(-30);
        }

        public static int hours = -5;
        public static int minutes = -30;

    }
}
