using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Common.Application.Windows.Expressions {
    public class Value : MarkupExtension, IExpression {
        public double? Double { get; set; }

        public Value() { }
        public Value(double @double)
            : this() {
            this.Double = @double;
        }

        public double CalculateValue() {
            if (Double == null) throw new System.Exception("Double");

            return Double.Value;
        }

        // Allows easy object instantiation in XAML attributes. (Result of StaticResource is not piped through ExpressionConverter.)
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
