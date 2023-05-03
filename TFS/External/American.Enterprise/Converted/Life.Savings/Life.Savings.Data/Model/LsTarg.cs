using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Life.Savings.Data.Model {
    public class LsTarg : ModelBase {
        public double MaleTarget { get; set; }
        public double FemaleTarget { get; set; }
        public static bool IsValid(IList<LsTarg> targRates, Gender gender, int age, out double returnRate)
        {
            returnRate = 0.0;
            if (!gender.IsSpecified || age > 70)
                return false;
            returnRate = gender.IsMale
                ? targRates[age + 1].MaleTarget : targRates[age + 1].FemaleTarget;
            return true;
        }
    }
}