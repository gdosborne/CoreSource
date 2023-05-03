using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model.Application {
    public class FutureWithdrawls {
        public int WD_Age { get; set; }
        public double WD_Amount { get; set; }
        public int WD_Years { get; set; }
        public int Loan_Age { get; set; }
        public double Loan_Amount { get; set; }
        public int Loan_Years { get; set; }
        public bool Loan_Interest { get; set; }
        public int LoanPay_Age { get; set; }
        public double LoanPay_Amount { get; set; }
        public int LoanPay_Years { get; set; }
        public bool IsInitialized { get; set; }
        public bool IsValid(int year) {
            var isValidAge = WD_Age > 0 && WD_Age <= year && (WD_Age + WD_Years - 1) >= year;
            var isValidLoadAge = Loan_Age > 0 && Loan_Age <= year && (Loan_Age + Loan_Years - 1) >= year;
            var isValidPay = LoanPay_Age > 0 && LoanPay_Age <= year && (LoanPay_Age + LoanPay_Years - 1) >= year;
            return isValidAge && isValidLoadAge && isValidPay;
        }
    }
}
