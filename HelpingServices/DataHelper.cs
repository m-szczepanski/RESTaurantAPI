using System;

namespace RESTaurantAPI.HelpingServices
{
    public class DataHelper
    {
        public static DateTime GetTodayDate()
        {
            return DateTime.Today;
        }

        public static string GetTodayDateString()
        {
            return DateTime.Today.ToString("yyyy-MM-dd");
        }
    }
}
