using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Life.Savings.Converters {
    public class CurrencyConverter : IValueConverter {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) return null;
            return double.Parse(GetNumbersAndDecimalOnly(value.ToString()));
        }
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return GetNumbersAndDecimalOnly(value.ToString());
        }
        protected string GetNumbersAndDecimalOnly(string original) {
            return new string(original.ToCharArray().Where(x => char.IsNumber(x) || x == '.').ToArray());
        }
    }
}
