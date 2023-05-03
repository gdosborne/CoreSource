using System;
using System.Globalization;
using System.Windows.Data;

namespace Life.Savings.Converters
{
    public class IntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var fraction = decimal.Parse(value.ToString());
            return fraction.ToString("0");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            value = value.ToString().Replace("..", ".");
            return value.ToString();
        }
    }
}
