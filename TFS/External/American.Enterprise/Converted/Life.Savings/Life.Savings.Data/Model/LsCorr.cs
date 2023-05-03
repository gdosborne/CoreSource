using System.Collections.Generic;
using System.Linq;

namespace Life.Savings.Data.Model {
    public class LsCorr : ModelBase
	{
		public double Value { get; set; }
        public static bool Verify(IList<LsCorr> rateValues, int age, out double returnRate)
        {
            returnRate = 0.0;
            var id = age > 95 ? 56 : age < 40 ? 1 : age - 39;
            var rate = rateValues.FirstOrDefault(x => x.Id == id);
            if (rate == null)
                return false;
            returnRate = rate.Value;
            return true;
        }
    }
}
