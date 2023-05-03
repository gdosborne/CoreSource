using System.Collections.Generic;

namespace Life.Savings.Data.Model {
    public class LsSurr : ModelBase
    {
        public LsSurr()
        {
            MaleSurr = new List<double>();
            FemaleSurr = new List<double>();
        }

        public IList<double> MaleSurr { get; private set; }
        public IList<double> FemaleSurr { get; private set; }
        public static bool Verify(IList<LsSurr> surrenderValues, CalculatingOnItems who, Gender gender, int age, int duration, out double returnRate)
        {
            returnRate = 0.0;
            if (!who.Equals(CalculatingOnItems.Principal) || !gender.IsSpecified || age > 69 || duration < 0 || (duration > surrenderValues[age + 1].MaleSurr.Count - 1))
                return false;
            returnRate = gender.IsMale
                ? surrenderValues[age + 1].MaleSurr[duration]
                : surrenderValues[age + 1].FemaleSurr[duration];
            return true;
        }
    }
}