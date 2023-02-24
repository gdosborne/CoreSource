using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Common.Application.Windows.Expressions {
    [TypeConverter(typeof(ExpressionConverter))]
    public interface IExpression {
        double CalculateValue();
    }
}
