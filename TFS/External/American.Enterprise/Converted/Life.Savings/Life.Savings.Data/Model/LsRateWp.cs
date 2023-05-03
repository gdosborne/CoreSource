using System.Collections.Generic;
using GregOsborne.Application.Primitives;
using System;

namespace Life.Savings.Data.Model
{
    public class LsRateWp : ModelBase
    {
        public double MaleWPD { get; set; }
        public double FemaleWPD { get; set; }
        public static bool IsValid(IList<LsRateWp> rateValues, Gender gender, int age, out double returnRate)
        {
            returnRate = 0.0;
            if (!gender.IsSpecified || !age.IsBetween(15, 59, false))
                return false;
            returnRate = gender.IsMale
                ? rateValues[age + 1].MaleWPD
                : rateValues[age + 1].FemaleWPD;
            return true;
        }
    }
}