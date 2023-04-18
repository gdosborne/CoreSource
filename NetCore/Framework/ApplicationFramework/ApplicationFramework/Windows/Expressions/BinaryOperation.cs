using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.OzApplication.Windows.Expressions {
    public abstract class BinaryOperation : IExpression {
        public IExpression Operand1 { get; set; }
        public IExpression Operand2 { get; set; }

        public double CalculateValue() {
            if (Operand1 == null) throw new System.Exception("Operand1 cannot be null.");
            if (Operand2 == null) throw new System.Exception("Operand2 cannot be null.");

            return CalculateBinaryOperation();
        }

        protected abstract double CalculateBinaryOperation();
    }
}
