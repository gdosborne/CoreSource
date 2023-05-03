using System.Collections.Generic;
using System.Linq;
using GregOsborne.Application.Primitives;
using System;

namespace Life.Savings.Data.Model
{
    public class LsCso80 : ModelBase
    {
        public double MaleNonSmoker { get; set; }
        public double MaleSmoker { get; set; }
        public double FemaleNonSmoker { get; set; }
        public double FemaleSmoker { get; set; }
        public static bool Verify(IList<LsCso80> rateValues, Gender gender, bool smoker, int age, out double returnRate)
        {
            returnRate = 0.0;
            if (!gender.IsSpecified || !age.IsBetween(15, 94, false)) return false;
            var id = age + 1;
            var rate = rateValues.FirstOrDefault(x => x.Id == id);
            if (rate == null) return false;
            returnRate = gender.IsMale
                ? (!smoker ? rate.MaleNonSmoker : rate.MaleSmoker)
                : (!smoker ? rate.FemaleNonSmoker : rate.FemaleSmoker);
            return true;
        }
    }
}