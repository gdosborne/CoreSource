using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model
{
    public class LsLedgerValues
    {
        public double AnnualOutlay { get; set; }
        public double SurrenderValue { get; set; }
        public double CashValue { get; set; }
        public double DeathBenefit { get; set; }
        public double WithdrawAmount { get; set; }
        public double LoanAmount { get; set; }
        public double LoanRepayAmount { get; set; }
        public double LoanBalance { get; set; }
    }
}
