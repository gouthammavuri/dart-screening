using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dart.screening.common
{
    public static class DateHelper
    {
        public static (bool,string) IsValidDate(string inputDate)
        {
            inputDate = inputDate.Trim();
            DateTime dateTime = DateTime.MinValue;
            if (DateTime.TryParseExact(inputDate, "MM/dd/yy", CultureInfo.InvariantCulture, DateTimeStyles.AllowInnerWhite, out dateTime))
                return (true, dateTime.ToString("yyyy-MM-dd"));
            else if (DateTime.TryParseExact(inputDate, "MMMM d, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowInnerWhite, out dateTime))
                return (true, dateTime.ToString("yyyy-MM-dd"));
            else if (DateTime.TryParseExact(inputDate, "MMM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowInnerWhite, out dateTime))
                return (true, dateTime.ToString("yyyy-MM-dd"));
            else 
                return (false, inputDate);
        }
    }
}
