using System;
using System.Globalization;
using System.Windows.Data;

namespace Life.Savings.Converters
{
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var fraction = decimal.Parse(value.ToString());
            return fraction.ToString("P2");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            value = value.ToString().Replace("..", ".");
            var valueWithoutPercentage = value.ToString().TrimEnd(' ', '%');
            return decimal.Parse(valueWithoutPercentage) / 100;
        }
    }
}
