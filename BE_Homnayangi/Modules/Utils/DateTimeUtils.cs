using System;
namespace BE_Homnayangi.Modules.Utils
{
    public static class DateTimeUtils
    {
        public static bool CheckValidFromAndToDate(DateTime fromDate, DateTime toDate)
        {
            var now = DateTime.Now;

            if (toDate.Date.CompareTo(now.Date) > 0) return false;

            if (fromDate.Date.CompareTo(toDate) > 0) return false;

            return true;
        }
    }
}

