using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models.Entities
{
	public class Publisher:BaseEntity
	{
		public static Publisher FromDatabase(T_Publisher x)
		{
			return new Publisher
			{
				FirstName = x.FirstName,
				LastName = x.Lastname,
				Special = x.Special
			};
		}
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool Special { get; set; }
	}
}
