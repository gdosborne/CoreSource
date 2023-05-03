using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model
{
    public class LsFutureWithDrawals
    {
        public int WD_Age { get; set; }
        public double WD_Amount { get; set; }
        public int WD_Years { get; set; }
        public int Loan_Age { get; set; }
        public double Loan_Amount { get; set; }
        public int Loan_Years { get; set; }
        public int Loan_Interest { get; set; }
        public int LoanPay_Age { get; set; }
        public double LoanPay_Amount { get; set; }
        public int LoanPay_Years { get; set; }
    }
}
