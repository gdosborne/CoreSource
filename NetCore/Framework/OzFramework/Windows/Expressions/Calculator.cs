/* File="Calculator"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.Primitives;
using System;
using System.Windows.Markup;

namespace Common.Windows.Expressions {
    [ContentProperty("Expression")]
    public class Calculator : MarkupExtension {
        public IExpression Expression { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            if (Expression.IsNull()) throw new System.Exception("Expression cannot be null.");
            return Expression.CalculateValue();
        }
    }
}
