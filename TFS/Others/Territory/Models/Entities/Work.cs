using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models.Entities
{
	public class Work : BaseEntity
	{
		public static Work FromDatabase(T_Work x)
		{
			return new Work
			{
				ID = x.ID,
				EndDate = x.EndDate,
				SpecialCampaign = SpecialCampaign.FromDatabase(x.T_SpecialCampaign),
				StartDate = x.StartDate,
				Publisher = Publisher.FromDatabase(x.T_Publisher)
			};
		}
		public DateTime? EndDate { get; set; }
		public DateTime StartDate { get; set; }
		public Publisher Publisher { get; set; }
		public SpecialCampaign SpecialCampaign { get; set; }
	}
}
