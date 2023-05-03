namespace MyMinistry.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class MyMinistryEntry
	{
		public DateTime EntryDateTime { get; set; }
		public TimeSpan? LengthOfTime { get; set; }
		public int? NumberOfVideos { get; set; }
		public int? TotalPlacements { get; set; }
	}
}
