using System.Collections.Generic;
using System.Linq;
using GregOsborne.Application.Primitives;
using System;

namespace Life.Savings.Data.Model
{
    public class LsRateSi : ModelBase
    {
        public LsRateSi()
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
        public static bool IsValid(IList<LsRateSi> rateValues, CalculatingOnItems who, Gender gender, bool smoker, int age, int band, out double returnRate)
        {
            returnRate = 0.0;
            if (!(who.Equals(CalculatingOnItems.Spouse) || who.Equals(CalculatingOnItems.Insured) || !gender.IsSpecified || age > 94 || !band.IsBetween(1, 2, true)))
                return false;
            var rate = rateValues.FirstOrDefault(x => x.Id == age + 1);
            if (rate == null)
                return false;
            if (gender.IsMale)
                returnRate = !smoker ? rate.MaleNonSmoker[band] : rate.MaleSmoker[band];
            else
                returnRate = !smoker ? rate.FemaleNonSmoker[band] : rate.FemaleSmoker[band];
            return true;
        }
    }
}