using Math = System.Math;

namespace VersionMaster {

    public static class UpdateMethods {
        private static Random randomGen = new Random();
        public static int Day() => DateTime.Now.Day;
        public static int DayValueFrom(DateTime minDate) => Math.Abs(Convert.ToInt32(Math.Floor(DateTime.Now.Subtract(minDate).TotalDays)));
        public static int Fixed(int current) => current;
        public static int Ignore(int current) => current;
        public static int Month() => DateTime.Now.Month;
        public static int Random() => randomGen.Next();
        public static int SecondValueFrom(DateTime minDate) => Convert.ToInt32(Math.Floor(DateTime.Now.Subtract(minDate).TotalSeconds));
		public static int Second() => DateTime.Now.Second;
        public static int Year() => DateTime.Now.Year;
        public static int TwoDigitYear() => int.Parse(Year().ToString("yy"));
        public static int Increment(int current, bool resetEachDay, DateTime? lastDate) {
            var result = current + 1;
            if (resetEachDay && lastDate.HasValue && lastDate.Value.Date < DateTime.Now.Date) {
                result = 0;
            }

            return result;
        }
        public static int IncrementResetEachDay(int current, DateTime? lastDate) {
            var result = current;
            if (!lastDate.HasValue) {
                throw new ApplicationException("Last date must be set.");
            }
			if (lastDate.Value.Date < DateTime.Now.Date) {
                result = 0;
            } else {
                result = current + 1;
            }

            return result;
        }
        public static int IncrementEachDay(int current, DateTime? lastDate) {
            var result = current;
            if (!lastDate.HasValue) {
                throw new ApplicationException("Last date must be set.");
            }

			if(current < DateTime.Now.Day || lastDate.Value.Date < DateTime.Now.Date) { 
            //if (lastDate.Value.Date < DateTime.Now.Date) {
                result = current + 1;
            }

            return result;
        }
    }
}
