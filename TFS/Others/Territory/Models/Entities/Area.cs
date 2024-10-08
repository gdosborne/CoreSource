using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models.Entities
{
	public class Area : BaseEntity
	{
		public static Area FromDataBase(T_Area x)
		{
			return new Area
			{
				ID = x.ID,
				Value = x.Value
			};			
		}
		public string Value { get; set; }
	}
}
