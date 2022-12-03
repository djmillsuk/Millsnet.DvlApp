using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millsnet.DvlApp.ValueConverters
{
    public class BoolConverter<T> : IValueConverter
    {
        public T TrueValue { get; set; }
        public T FalseValue { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return TrueValue;
            }
            return FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is T)
            {
                return true;
            }
            return false;
        }
    }
}
