/* File="Value"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.Primitives;
using System;
using System.Windows.Markup;

namespace Common.Windows.Expressions {
    public class Value : MarkupExtension, IExpression {
        public double? Double { get; set; }

        public Value() { }
        public Value(double @double)
            : this() {
            this.Double = @double;
        }

        public double CalculateValue() {
            if (Double.IsNull()) throw new System.Exception("Double");

            return Double.Value;
        }

        // Allows easy object instantiation in XAML attributes. (Result of StaticResource is not piped through ExpressionConverter.)
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
