namespace OSControls
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Media;

	public static class Extensions
	{
		#region Public Methods
		public static int JulianDate(this DateTime theDate)
		{
			var y = theDate.Year;
			var m = theDate.Month;
			var d = theDate.Day;

			int yy = y - (int)((12 - m) / 10);
			int mm = m + 9;
			if (mm >= 12)
				mm = mm - 12;
			int k1 = (int)(365.25 * (yy + 4712));
			int k2 = (int)(30.6001 * m + 0.5);
			int k3 = (int)((int)((yy / 100) + 49) * 0.75) - 38;
			int j = k1 + k2 + d + 59;
			if (j > 2299160)
				j = j - k3;
			return j;
		}
		public static int MoonPhaseInt(this DateTime theDate, int numPhases)
		{
			var y = theDate.Year;
			var m = theDate.Month;
			var d = theDate.Day;
			var days = MoonAge(d, m, y);
			var days1 = (double)MoonAge1(theDate);
			var result = (int)Math.Round(days1 / (MoonDays / numPhases));
			//return moon_phase(y, m, d);
			//return MoonAge(d, m, y);
			//int tdy, tdm;
			//double totalDays;
			//int result;

			//if (m < 3)
			//{
			//	y--;
			//	m += 12;
			//}
			//++m;
			//tdy = (int)365.25 * y;
			//tdm = (int)30.6 * m;

			//totalDays = tdy + tdm + d - 694039.09;			//totalDays is total days elapsed
			//totalDays /= 29.53;								//divide by the moon cycle (29.53 days)
			//result = (int)totalDays;						//int(totalDays) -> b, take integer part of totalDays
			//totalDays -= result;							//subtract integer part to leave fractional part of original totalDays
			//result = (int)((totalDays * numPhases) + 0.5);	//scale fraction from 0-numPhases and round by adding 0.5
			result = result & (numPhases - 1);				//0 and numPhases are the same so turn numPhases into 0
			return result;
		}
		#endregion Public Methods

		#region Private Methods
		private static int moon_phase(int y, int m, int d)
		{
			/*
			  calculates the moon phase (0-7), accurate to 1 segment.
			  0 = > new moon.
			  4 => full moon.
			  */

			//int c, e;
			//double jd;
			//int b;

			if (m < 3)
			{
				y--;
				m += 12;
			}
			++m;
			var c = 365.25 * y;
			var e = 30.6 * m;
			var jd = c + e + d - 694039.09;  /* jd is total days elapsed */
			jd /= 29.53;           /* divide by the moon cycle (29.53 days) */
			var b = jd;		   /* int(jd) -> b, take integer part of jd */
			jd -= b;		   /* subtract integer part to leave fractional part of original jd */
			b = jd * 8 + 0.5;	   /* scale fraction from 0-8 and round by adding 0.5 */
			b = (int)b & 7;		   /* 0 and 8 are the same so turn 8 into 0 */
			return (int)b;
		}
		private static double MoonAge(int d, int m, int y)
		{
			int j = JulianDate(new DateTime(y, m, d));
			//Calculate the approximate phase of the moon
			var ip = (j + 4.867) / 29.53059;
			ip = ip - Math.Floor(ip);
			//After several trials I've seen to add the following lines,
			//which gave the result was not bad
			var ag = 0.0;
			if (ip < 0.5)
				ag = ip * 29.53059 + 29.53059 / 2;
			else
				ag = ip * 29.53059 - 29.53059 / 2;
			// Moon's age in days
			ag = Math.Floor(ag) + 1;
			return ag;
		}
		private static int MoonAge1(DateTime theDate)
		{
			var moonBaseDate = new DateTime(1899, 12, 31); //1-1-1900 was new moon
			var numDaysSince = theDate.Subtract(moonBaseDate).TotalDays - 1;
			return (int)Math.Round(numDaysSince % MoonDays);
		}
		#endregion Private Methods

		#region Private Fields
		private static readonly double MoonDays = 29.530588853;
		#endregion Private Fields
	}
}
