using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Life.Savings.Converters
{
    public class TelephoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var telephone = GetTelephoneString((string)value);
            if (telephone.Length == 10)
                return $"({telephone.Substring(0, 3)}) {telephone.Substring(3, 3)}-{telephone.Substring(6, 4)}";
            else if (telephone.Length == 7)
                return $"({telephone.Substring(0, 3)}-{telephone.Substring(3, 4)}";
            else
                return telephone;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetTelephoneString((string)value);
        }
        private string GetTelephoneString(string original)
        {
            var value = new string(original.ToCharArray().Where(x => char.IsNumber(x)).ToArray());
            return value;
        }
    }
}
