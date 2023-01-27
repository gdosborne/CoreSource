using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Applicationn.Primitives {
    public static class DataValues {
        public static IEnumerable<TimeDisplayValue> TimeValues() => TimeValues(30);
        public static IEnumerable<TimeDisplayValue> TimeValues(int incrementMinutes) => TimeValues(incrementMinutes, new TimeSpan(0, incrementMinutes, 0), new TimeSpan(24, 0, 0));
        public static IEnumerable<TimeDisplayValue> TimeValues(int incrementMinutes, TimeSpan start, TimeSpan end) {
            var t = start;
            if (end < start) {
                throw new ArgumentException("Start must be before end");
            }
            var result = new List<TimeDisplayValue>();
            if (end == start) {
                result.Add(new TimeDisplayValue(t));
                return result;
            }
            while (t <= end) {
                result.Add(new TimeDisplayValue(t));
                t = t.Add(new TimeSpan(0, incrementMinutes, 0));
            }
            return result;
        }
    }

    public class TimeDisplayValue {
        public TimeDisplayValue(TimeSpan value) => Value = value;
        public TimeDisplayValue(DateTime value) => Value = value.TimeOfDay;
        public TimeSpan Value { get; set; }
        public string DisplayValue => new DateTime(Value.Ticks).ToString("h:mm tt");
        
    }
}
