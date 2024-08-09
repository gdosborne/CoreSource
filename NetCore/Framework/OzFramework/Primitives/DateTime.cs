/* File="DateTime"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

namespace Common.CCCSystem {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using static Common.CCCSystem.DateTime;

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
            return Enum.TryParse<MonthsOfTheYear>(value, out var yearValue)
                ? (int)yearValue : 0;
        }
    }

    public class DateOnly {
        public DateOnly()
            : this(CultureInfo.CurrentCulture) {
        }
        public DateOnly(CultureInfo cultureInfo)
            : this(System.DateTime.Now.ToString(), cultureInfo) { }

        public DateOnly(string dateAsString)
            : this(dateAsString, CultureInfo.CurrentCulture) { }
        public DateOnly(System.DateTime date)
            : this(date.ToString(), CultureInfo.CurrentCulture) { }

        public DateOnly(string dateAsString, CultureInfo cultureInfo) {
            if (System.DateTime.TryParse(dateAsString, out var date)) {
                this.date = date;
            }
        }
        public DateOnly(System.DateTime date, CultureInfo cultureInfo) => this.date = date;

        private DateOnly Go(System.DateTime theDate, CultureInfo cultureInfo) => new(theDate, cultureInfo);

        private System.DateTime date = default;
        public CultureInfo CultureInfo { get; internal set; }
        public string MonthName => CultureInfo.DateTimeFormat.GetMonthName(date.Month);
        public MonthsOfTheYear MonthOfTheYear => (MonthsOfTheYear)date.Month;
        public int Day => date.Day;
        public int Year => date.Year;
        public int Month => date.Month;
        public override string ToString() => date.ToString(CultureInfo.DateTimeFormat.ShortDatePattern);
        public DateOnly AddDays(int days) => Go(this.date.AddDays(days), CultureInfo);
        public DateOnly AddMonths(int months) => Go(this.date.AddMonths(months), CultureInfo);
        public DateOnly SubtractDays(int days) => Go(this.date.AddDays(-days), CultureInfo);
        public DateOnly SubtractMonths(int months) => Go(this.date.AddMonths(-months), CultureInfo);
        
    }
}
