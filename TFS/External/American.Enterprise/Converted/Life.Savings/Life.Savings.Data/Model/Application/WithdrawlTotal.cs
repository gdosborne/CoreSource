namespace Life.Savings.Data.Model.Application {
    public class WithdrawlTotal {
        public bool IsDefined { get { return CashValue > 0 || FaceValue > 0 || DeathBenefit > 0 || MinimumDeathBenefit > 0 || LoanBalance > 0; } }
        public double CashValue { get; set; }
        public double FaceValue { get; set; }
        public double DeathBenefit { get; set; }
        public double MinimumDeathBenefit { get; set; }
        public double LoanBalance { get; set; }
    }
}
