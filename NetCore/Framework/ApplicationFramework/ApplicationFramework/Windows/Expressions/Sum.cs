using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.OzApplication.Windows.Expressions {
    public class Sum : BinaryOperation {
        protected override double CalculateBinaryOperation() =>
            Operand1.CalculateValue() + Operand2.CalculateValue();
    }
}
