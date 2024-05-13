/* File="BinaryOperation"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzFramework.Windows.Expressions {
    public abstract class BinaryOperation : IExpression {
        public IExpression Operand1 { get; set; }
        public IExpression Operand2 { get; set; }

        public double CalculateValue() {
            if (Operand1.IsNull()) throw new System.Exception("Operand1 cannot be null.");
            if (Operand2.IsNull()) throw new System.Exception("Operand2 cannot be null.");

            return CalculateBinaryOperation();
        }

        protected abstract double CalculateBinaryOperation();
    }
}
