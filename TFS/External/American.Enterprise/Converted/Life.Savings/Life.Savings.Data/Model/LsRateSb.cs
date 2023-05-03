using System;
using System.Collections.Generic;
using System.Linq;

namespace Life.Savings.Data.Model {
    public class LsRateSb : ModelBase {
        public double SmokerSubstandard { get; set; }
        public double NonSmokerSubstandard { get; set; }
        public static bool IsValid(IList<LsRateSb> rateValues, bool smoker, int age, out double returnRate) {
            returnRate = 0.0;
            if (age > 94)
                return false;
            var rate = rateValues.FirstOrDefault(x => x.Id == age + 1);
            if (rate == null)
                return false;
            returnRate = !smoker ? rate.NonSmokerSubstandard : rate.SmokerSubstandard;
            return true;
        }
    }
}