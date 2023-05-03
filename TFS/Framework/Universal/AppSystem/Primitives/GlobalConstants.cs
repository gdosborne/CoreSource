namespace AppSystem.Primitives {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public static class GlobalConstants {
		private const double oneKb = 1024.0;
		public static double OneMegaByte => oneKb * 1000;
		public static double OneGigaByte => OneMegaByte * 1000;
		public static double OneTeraByte => OneGigaByte * 1000;

        public enum TimeIntervals {
            QuarterHour,
            HalfHour,
            FullHour
        }

        public static IList<DayOfWeek> WeekDays = new List<DayOfWeek> {
            DayOfWeek.Sunday,
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday
        };

        public static IList<string> DayTimeValues(TimeIntervals interval, TimeSpan start, TimeSpan end) {
            var result = new List<string>();
            var current = start;
            var addAmount = interval == TimeIntervals.QuarterHour
                ? TimeSpan.FromMinutes(15)
                : interval == TimeIntervals.HalfHour
                    ? TimeSpan.FromMinutes(30)
                    : TimeSpan.FromMinutes(60);
            while (true) {
                if (current > end)
                    break;
                var hr = default(int);
                var min = default(string);
                var ap = default(string);
                if (current.TotalHours == 24) {
                    hr = 12;
                    min = "00";
                    ap = "am";
                }
                else {
                    hr = current.Hours < 13 ? current.Hours : current.Hours - 12;
                    ap = current.Hours < 12 ? "am" : "pm";
                    min = current.Minutes.ToString("00");
                }
                result.Add($"{hr}:{min} {ap}");
                current = current.Add(addAmount);
            }
            return result;
        }
	}
}
