using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Life.Savings.Data.Model;
using Life.Savings.Data.Model.Application;

namespace Life.Savings.Data {
    public class CalculationValues {
        public CalculationValues() {
            CashValue = new double[] { 0.0, 0.0 };
        }
        public RateTypes CurrentRateType { get; set; }
        public double GetCashValue(RateTypes rateType) {
            return CashValue[(int)rateType];
        }
        public double GetCashValue() {
            return GetCashValue(CurrentRateType);
        }
        private double[] CashValue { get; set; }
        public int PrincipalBand { get; set; }
        public IllustrationInfo Info { get; set; }
        public IRepository Repo { get; set; }
        public IAppData DataSet { get; set; }
        public IList<MortalityItem> ClientRates { get; set; }
        public double InsuredMortCosts { get; set; }
        public double MortalityRate { get; set; }
    }
}
