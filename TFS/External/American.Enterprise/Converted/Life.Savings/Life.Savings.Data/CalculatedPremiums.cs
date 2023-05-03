using System.Collections.Generic;
using Life.Savings.Data.Model;
using Life.Savings.Data.Model.Application;

namespace Life.Savings.Data {
    public class CalculatedPremiums {
        public CalculatedPremiums(int startAge) {
            InsdMin = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            InsdTarg = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            LedgerItems = LedgerValue.LedgerItems(startAge);
        }
        public double PrinMin { get; set; }
        public double SpouseMin { get; set; }
        public double ChildMin { get; set; }
        public double[] InsdMin { get; set; }
        public double PrinTarg { get; set; }
        public double SpouseTarg { get; set; }
        public double ChildTarg { get; set; }
        public double[] InsdTarg { get; set; }
        public double TotMin { get; set; }
        public double TotTarg { get; set; }
        public double GuideAnnual { get; set; }
        public double GuideSingle { get; set; }
        public double GuideSingleAddInsd { get; set; }
        public string InForceYears { get; set; }
        public string SolvePremium { get; set; }
        public IList<MortalityItem> ClientGuaranteed { get; set; }
        public IList<MortalityItem> ClientProjected { get; set; }
        public IList<LedgerValue> LedgerItems { get; private set; }
    }
}
