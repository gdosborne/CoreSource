using System.Collections.Generic;
using System.Linq;
using GregOsborne.Application.Primitives;
using System;

namespace Life.Savings.Data.Model {
    public class WeightMinMax : ModelBase {
        public WeightMinMax() {
            WeightValues = new List<WeightValue>();
        }
        public IList<WeightValue> WeightValues { get; }
        public static bool Verify(IList<WeightMinMax> rateValues, int inches, double pounds, int age, out int lifeWtValue) {
            lifeWtValue = 0;
            if (age < 15)
                return false;
            if (age.IsBetween(15, 18, true))
            {
                inches = age < 18 ? inches - 1 : inches;
                if (!inches.IsBetween(56, 80, false))
                    return false;
            }

            var rate = rateValues.FirstOrDefault(x => x.Id == inches - 55);
            var weightRange = rate?.WeightValues.FirstOrDefault(x => string.IsNullOrEmpty(x.Key));
            if (weightRange == null || !pounds.IsBetween(weightRange.Min, weightRange.Max, false))
                return false;
            var keys = new[] {"0", "2", "3", "4", "5", "6", "8", "10", "12"};
            foreach (var key in keys) {
                weightRange = rate.WeightValues.FirstOrDefault(x => x.Key.Equals(key));
                if (weightRange == null || !pounds.IsBetween(weightRange.Min, weightRange.Max, true)) continue;
                lifeWtValue = int.Parse(key);
                return true;
            }
            return false;
        }
    }

    public class WeightValue {
        public string Key { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
    }
}