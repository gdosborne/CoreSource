using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model
{
	public class LsRateGp : ModelBase
	{
		public double Value { get; set; }
        public static bool IsValid(IList<LsRateGp> rateValues, int age, out double returnRate)
        {
            returnRate = 0.0;
            if (age > 40)
                return false;
            var rate = rateValues.FirstOrDefault(x => x.Id == age + 1);
            if (rate == null)
                return false;
            returnRate = rate.Value;
            return true;
        }
    }
}
