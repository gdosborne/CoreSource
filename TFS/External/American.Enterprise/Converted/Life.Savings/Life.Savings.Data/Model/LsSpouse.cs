using System;
using System.Collections.Generic;
using System.Linq;

namespace Life.Savings.Data.Model {
    public class LsSpouse : ModelBase {
        public double Value { get; set; }
        public static bool Verify(IList<LsSpouse> spouseValues, int age, out double returnRate) {
            returnRate = 0.0;
            if (age > 95) {
                var spouse = spouseValues.FirstOrDefault(x => x.Id == 31);
                if (spouse != null) returnRate = spouse.Value;
            }
            else if (age < 65) {
                returnRate = 1.0;
            }
            else {
                var spouse = spouseValues.FirstOrDefault(x => x.Id == age - 64);
                if (spouse != null) returnRate = spouse.Value;
            }
            return true;
        }
    }
}