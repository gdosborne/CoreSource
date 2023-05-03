using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GregOsborne.Application.Primitives;
using System.IO;
using System.Xml.Linq;

namespace Life.Savings.Data.Model
{
    public class LsMinp : ModelBase
    {
        public LsMinp()
        {
            MaleNonSmokerMinp = new List<double>();
            MaleSmokerMinp = new List<double>();
            FemaleNonSmokerMinp = new List<double>();
            FemaleSmokerMinp = new List<double>();
        }
        public IList<double> MaleNonSmokerMinp { get; private set; }
        public IList<double> MaleSmokerMinp { get; private set; }
        public IList<double> FemaleNonSmokerMinp { get; private set; }
        public IList<double> FemaleSmokerMinp { get; private set; }

        public static bool IsValid(IList<LsMinp> rateValues, Gender gender, bool smoker, int band, int age, out double returnRate)
        {
            returnRate = 0.0;
            if (!gender.IsSpecified || age > 70 || !band.IsBetween(0, 4, false))
                return false;
            var rate = rateValues.FirstOrDefault(x => x.Id == age + 1);
            if (rate == null)
                return false;
            returnRate = gender.IsMale
                ? (!smoker ? rate.MaleNonSmokerMinp[band] : rate.MaleSmokerMinp[band])
                : (!smoker ? rate.FemaleNonSmokerMinp[band] : rate.FemaleSmokerMinp[band]);
            return true;
        }       
    }
}
