using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Events
{
    public class AddGuaranteeProjectedValueRowEventArgs : EventArgs
    {
        public delegate void AddGuaranteeProjectedValueRowHandler(object sender, AddGuaranteeProjectedValueRowEventArgs e);
        public AddGuaranteeProjectedValueRowEventArgs(int age, int endOfYear, int annualOutlay, int guarnteedCashSurrenderValue, int guarnteedCashValue, int guarnteedDeathBenefit, int projectedCashSurrenderValue, int projectedCashValue, int projectedDeathBenefit)
        {
            Age = age;
            EndOfYear = endOfYear;
            AnnualOutlay = annualOutlay;
            GuarnteedCashSurrenderValue = guarnteedCashSurrenderValue;
            GuarnteedCashValue = guarnteedCashValue;
            GuarnteedDeathBenefit = guarnteedDeathBenefit;
            ProjectedCashSurrenderValue = projectedCashSurrenderValue;
            ProjectedCashValue = projectedCashValue;
            ProjectedDeathBenefit = projectedDeathBenefit;
        }
        public int Age { get; private set; }
        public int EndOfYear { get; private set; }
        public int AnnualOutlay { get; private set; }
        public int GuarnteedCashSurrenderValue { get; private set; }
        public int GuarnteedCashValue { get; private set; }
        public int GuarnteedDeathBenefit { get; private set; }
        public int ProjectedCashSurrenderValue { get; private set; }
        public int ProjectedCashValue { get; private set; }
        public int ProjectedDeathBenefit { get; private set; }
    }
}
