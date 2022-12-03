using System.Globalization;

namespace Millsnet.DvlApp.ValueConverters
{
    // All the code in this file is included in all platforms.
    public class DateTimeToStringConverter : IValueConverter
    {
        public DateConversionResultType ResultType { get; set; }
        public int DayOffset { get; set; } = 0;
        public int MonthOffset { get; set; } = 0;
        public int YearOffset { get; set; } = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime? && value != null)
            {
                var date = ((DateTime?)value).Value.Date;
                date.AddYears(YearOffset);
                date.AddMonths(MonthOffset);
                date.AddDays(DayOffset);
                switch (ResultType)
                {
                    case DateConversionResultType.DateOnly:
                        return date.ToString("dd/MM/yyyy");
                    case DateConversionResultType.LongDate:
                        return date.ToString("ddd MMM dd yyyy");
                    case DateConversionResultType.FriendlyDescription:
                        if (date == default)
                            return "Never";
                        else if (DateTime.Today == date)
                            return "Today";
                        else if (DateTime.Today.AddDays(-1) == date)
                            return "Yesterday";
                        else if (DateTime.Today.AddDays(0 - DateTime.Today.DayOfWeek) < date)
                            return date.DayOfWeek.ToString();
                        else if (DateTime.Today.AddDays(0 - (DateTime.Today.DayOfWeek + 7)) < date)
                            return $"Last {date.DayOfWeek.ToString()}";
                        else if (DateTime.Today.AddDays(-30) > date)
                            return $"{(DateTime.Today - date.Date).TotalDays} days ago";
                        else if (DateTime.Today.AddYears(-1) > date)
                            return $"{System.Convert.ToInt32(Math.Round((DateTime.Today - date.Date).TotalDays / 30, 0))} months ago";
                        else
                            return date.ToString("ddd dd/MM/yy");
                    case DateConversionResultType.TimeOnly:
                        return date.ToString("HH:mm");
                    default:
                        return date.ToString();
                }
            }
            return "Invalid Date";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string v = value as string;
            if (v == "Never")
                return null;
            else return DateTimeOffset.ParseExact(v, null, CultureInfo.InvariantCulture);

        }
    }

    public enum DateConversionResultType
    {
        FriendlyDescription,
        DateOnly,
        LongDate,
        TimeOnly
    }
}