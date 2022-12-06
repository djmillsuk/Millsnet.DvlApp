using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millsnet.DvlApp.ValueConverters
{
    public class StringConverter : IValueConverter
    {
        public List<string> SourceValues { get; set; } = new List<string>();
        public List<string> TargetValues { get; set; } = new List<string>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (SourceValues.Contains(value.ToString()))
                {
                    int index = SourceValues.ToList().IndexOf(value.ToString());
                    return TargetValues[index];
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (TargetValues.Contains(value.ToString()))
                {
                    int index = TargetValues.ToList().IndexOf(value.ToString());
                    return SourceValues[index];
                }
            }
            return string.Empty;
        }
    }
}
