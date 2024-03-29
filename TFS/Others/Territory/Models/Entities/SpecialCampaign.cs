using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models.Entities
{
	public class SpecialCampaign : BaseEntity
	{
		public static SpecialCampaign FromDatabase(T_SpecialCampaign x)
		{
			if (x == null)
				return null;
			return new SpecialCampaign
			{
				ID = x.ID,
				Name = x.Name
			};
		}
		public string Name { get; set; }
	}
}
