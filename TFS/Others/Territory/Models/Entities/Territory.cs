using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models.Entities
{
	public class Territory : BaseEntity
	{
		public static Territory FromDataBase(T_Territory x)
		{
			var result = new Territory
			{
				ID = x.ID,
				Area = Area.FromDataBase(x.T_Area),
				Number = x.Number,
				OtherFileName = x.OtherFileName,
				TerritoryType = TerritoryType.FromDatabase(x.T_TerritoryType),
				Works = new List<Work>()
			};
			x.T_Works.ToList().ForEach(y => result.Works.Add(Work.FromDatabase(y)));
			return result;
		}
		public Area Area { get; set; }
		public string Number { get; set; }
		public string FileName { get; set; }
		public string OtherFileName { get; set; }
		public TerritoryType TerritoryType { get; set; }
		public List<Work> Works { get; private set; }
		public DateTime? LastWorked
		{
			get
			{
				if (Works.Any(x => x.EndDate.HasValue))
					return Works.Where(x => x.EndDate.HasValue).OrderByDescending(x => x.EndDate.Value).First().EndDate.Value;
				else
					return null;
			}
		}
		public DateTime? CheckedOutDate
		{
			get
			{
				if (Works.Any(x => !x.EndDate.HasValue))
					return Works.LastOrDefault(x => !x.EndDate.HasValue).StartDate;
				else
					return null;
			}
		}
		public int NumberOfDaysSinceWorked
		{
			get
			{
				if (Works.Any(x => x.EndDate.HasValue))
					return Convert.ToInt32(DateTime.Now.Subtract(Works.Where(x => x.EndDate.HasValue).OrderByDescending(x => x.EndDate.Value).First().EndDate.Value).TotalDays);
				else
					return 0;
			}
		}
		public int CheckedOutDays
		{
			get
			{
				if (Works.Any(x => !x.EndDate.HasValue))
					return Convert.ToInt32(DateTime.Now.Subtract(Works.LastOrDefault(x => !x.EndDate.HasValue).StartDate).TotalDays);
				else
					return 0;
			}
		}
		public string PublisherName
		{
			get
			{
				if (Works.Any(x => !x.EndDate.HasValue))
					return string.Format("{0} {1}", Works.LastOrDefault(x => !x.EndDate.HasValue).Publisher.FirstName, Works.LastOrDefault(x => !x.EndDate.HasValue).Publisher.FirstName);
				else
					return string.Empty;
			}
		}
	}
}
