using System.Collections.Generic;

namespace Life.Savings.Data.Model.Application {
    public class LedgerValue {
        private static IList<LedgerValue> _LedgerItems = null;
        public static IList<LedgerValue> LedgerItems(int age) {
            if (_LedgerItems == null)
            {
                _LedgerItems = new List<LedgerValue>();
                for (int i = 1; i < 99; i++)
                {
                    _LedgerItems.Add(new LedgerValue() {
                        Year = i,
                        EndOfYear = i - age < 0 ? 0 : i - age
                    });
                }
            }
            return _LedgerItems;
        }
        public LedgerValue() {
            SurrenderValue = new double[] { 0, 0 };
            CashValue = new double[] { 0, 0 };
            DeathBenefit = new double[] { 0, 0 };
        }
        public double AnnualOutlay { get; set; }
        public double[] SurrenderValue { get; set; }
        public double[] CashValue { get; set; }
        public double[] DeathBenefit { get; set; }
        public double WithdrawAmount { get; set; }
        public double LoanAmount { get; set; }
        public double LoanRepayAmount { get; set; }
        public double LoanBalance { get; set; }
        public int Year { get; set; }
        public int EndOfYear { get; set; }
    }
}
