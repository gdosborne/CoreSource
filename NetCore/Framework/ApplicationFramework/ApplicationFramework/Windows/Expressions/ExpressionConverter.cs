using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Windows.Expressions {
    public class ExpressionConverter : DoubleConverter {
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
            var doubleValue = (double)base.ConvertFrom(context, culture, value);
            return (IExpression)new Value(doubleValue);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) {
            var val = (Value)value;
            return base.ConvertTo(context, culture, val.CalculateValue(), destinationType);
        }
    }
}
