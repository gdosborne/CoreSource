
using System;
using System.Collections.Generic;
using System.Linq;

namespace Life.Savings.Data.Model
{
    public class SimpleValue : ModelBase
    {
        internal static IDictionary<double, string> NegativeValues = null;
        private double _Value;
        public double Value { get => _Value; set => _Value = value; }
        public override string ToString()
        {
            return DisplayedValue;
        }
        public string DisplayedValue {
            get {
                if (Value < 0)
                {
                    if (NegativeValues.ContainsKey(Value))
                        return NegativeValues[Value];
                    else
                        throw new ApplicationException($"Invalid value {Value}");
                }
                else
                    return Value < 1 ? Value.ToString("0.0%") : Value.ToString("N2");
            }
            set {
                if (!NegativeValues.Any(x => x.Value == value))
                {
                    if (double.TryParse(value, out var t))
                        Value = t;
                    else
                        throw new ApplicationException($"Invalid value {value}");
                }
                else
                    Value = NegativeValues.First(x => x.Value == value).Key;
            }
        }
    }
    public class YearsToPaySimpleValue: SimpleValue
    {
        public void UpdateYearsToPayDisplayValue(int numberOfYears)
        {
            NegativeValues[-14] = $"{numberOfYears} Years";
        }
    }
}
