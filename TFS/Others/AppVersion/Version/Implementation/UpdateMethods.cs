namespace VersionEngine.Implementation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class UpdateMethods
	{
		#region Public Methods
		public static int Day() {
			return DateTime.Now.Day;
		}
		public static int Fixed(int value) {
			return value;
		}
		public static int Ignore(int current) {
			return current;
		}
		public static int Increment(int current) {
			return current + 1;
		}
		public static int IncrementResetEachDay(int current, DateTime? lastUpdate) {
			if (!lastUpdate.HasValue || DateTime.Now.Date > lastUpdate.Value.Date)
				return 0;
			return Increment(current);
		}
		public static int Month() {
			return DateTime.Now.Month;
		}
		public static int Second() {
			return DateTime.Now.Second;
		}
		public static int Year() {
			return DateTime.Now.Year;
		}
		public static int Year2Digit() {
			return int.Parse(DateTime.Now.ToString("yy"));
		}
		#endregion Public Methods
	}
}
