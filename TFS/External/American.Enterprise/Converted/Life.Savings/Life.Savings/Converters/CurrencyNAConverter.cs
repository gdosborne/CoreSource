using System;
using System.Globalization;

namespace Life.Savings.Converters {
    public class CurrencyNAConverter : CurrencyConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value.ToString() == "N/A" || System.Convert.ToDouble(value) == 0.0) return "N/A";
            return base.Convert(value, targetType, parameter, culture);
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if ((string)value == "N/A") return 0.0;
            return base.ConvertBack(value, targetType, parameter, culture);
        }
    }
}
