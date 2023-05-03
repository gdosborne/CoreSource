using GregOsborne.Application.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Life.Savings.Data.Model
{
    public class LsRatePr : ModelBase
    {
        public LsRatePr()
        {
            MaleNonSmoker = new List<double>();
            MaleSmoker = new List<double>();
            FemaleNonSmoker = new List<double>();
            FemaleSmoker = new List<double>();
        }
        public IList<double> MaleNonSmoker { get; }
        public IList<double> MaleSmoker { get; }
        public IList<double> FemaleNonSmoker { get; }
        public IList<double> FemaleSmoker { get; }
        public static bool IsValid(IList<LsRatePr> rateValues, CalculatingOnItems who, Gender gender, bool smoker, int band, int age, out double returnRate)
        {
            returnRate = 0.0;
            if (!who.Equals(CalculatingOnItems.Principal) || !gender.IsSpecified || age > 94 || !band.IsBetween(0, 4, true))
                return false;
            var rate = rateValues.FirstOrDefault(x => x.Id == age + 1);
            if (rate == null)
                return false;
            returnRate = gender.IsMale
                ? (!smoker ? rate.MaleNonSmoker[band] : rate.MaleSmoker[band])
                : (!smoker ? rate.FemaleNonSmoker[band] : rate.FemaleSmoker[band]);
            return true;
        }
    }
}
