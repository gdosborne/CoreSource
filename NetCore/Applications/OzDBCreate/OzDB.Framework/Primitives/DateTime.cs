namespace OzDB.Application.OSSystem {
    using System.Collections.Generic;
    using System;

    public static class DateTime {
        public enum MonthsOfTheYear {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        public static IEnumerable<string> Months {
            get {
                return Enum.GetNames(typeof(MonthsOfTheYear));
            }
        }

        public static int ToMonthValue(this string value) {
            if (Enum.TryParse<MonthsOfTheYear>(value, out var yearValue)) {
                return (int)yearValue;
            }
            else
                return 0;
        }

    }
}
