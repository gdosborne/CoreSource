using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model.Application {
    internal class YearlyData {
        public int DeathBenefitOption { get; set; }
        public double CashValue { get; set; }
        public double MonthlyPremium { get; set; }
        public double Outlay { get; set; }
        public double Charges { get; set; }
        public double FaceValue { get; set; }
        public double InsuredMortalityCosts { get; set; }
        public double LoanBalance { get; set; }
        public double ModalPremium { get; set; }
        public double ClientCola { get; set; }
        public double SpouseCola { get; set; }
        public double InterestRate { get; set; }
        public double DeathBenefit { get; set; }
        public double StartCashValue { get; set; }
        public double MinimumDeathBenefit { get; set; }
    }
}
