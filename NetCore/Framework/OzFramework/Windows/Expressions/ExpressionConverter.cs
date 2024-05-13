/* File="ExpressionConverter"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzFramework.Windows.Expressions {
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
