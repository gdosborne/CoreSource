/* File="IExpression"
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
using System.Windows;

namespace Common.Windows.Expressions {
    [TypeConverter(typeof(ExpressionConverter))]
    public interface IExpression {
        double CalculateValue();
    }
}
