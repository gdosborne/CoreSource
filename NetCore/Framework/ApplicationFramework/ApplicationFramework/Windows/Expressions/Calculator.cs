using System;
using System.Windows.Markup;

namespace Common.Applicationn.Windows.Expressions {
    [ContentProperty("Expression")]
    public class Calculator : MarkupExtension {
        public IExpression Expression { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            if (Expression == null) throw new System.Exception("Expression cannot be null.");
            return Expression.CalculateValue();
        }
    }
}
