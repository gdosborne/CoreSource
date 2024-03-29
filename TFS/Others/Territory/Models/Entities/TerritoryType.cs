using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models.Entities
{
	public class TerritoryType : BaseEntity
	{
		public static TerritoryType FromDatabase(T_TerritoryType x)
		{
			return new TerritoryType
			{
				ID = x.ID,
				Name = x.NAME
			};
		}
		public string Name { get; set; }
	}
}
