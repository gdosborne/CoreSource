using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMinistry.Utilities
{
	public class StateItem
	{
		public static IList<StateItem> GetStates()
		{
			var result = new List<StateItem>
			{
				new StateItem{ Name="Alabama", Abbreviation="AL" },
				new StateItem{ Name="Alaska", Abbreviation="AK" },
				new StateItem{ Name="Arizona", Abbreviation="AZ" },
				new StateItem{ Name="Arkansas", Abbreviation="AR" },
				new StateItem{ Name="California", Abbreviation="CA" },
				new StateItem{ Name="Colorado", Abbreviation="CO" },
				new StateItem{ Name="Connecticut", Abbreviation="CT" },
				new StateItem{ Name="Delaware", Abbreviation="DE" },
				new StateItem{ Name="Florida", Abbreviation="FL" },
				new StateItem{ Name="Georgia", Abbreviation="GA" },
				new StateItem{ Name="Hawaii", Abbreviation="HI" },
				new StateItem{ Name="Idaho", Abbreviation="ID" },
				new StateItem{ Name="Illinois", Abbreviation="IL" },
				new StateItem{ Name="Indiana", Abbreviation="IN" },
				new StateItem{ Name="Iowa", Abbreviation="IA" },
				new StateItem{ Name="Kansas", Abbreviation="KS" },
				new StateItem{ Name="Kentucky", Abbreviation="KY" },
				new StateItem{ Name="Louisiana", Abbreviation="LA" },
				new StateItem{ Name="Maine", Abbreviation="ME" },
				new StateItem{ Name="Maryland", Abbreviation="MD" },
				new StateItem{ Name="Massachusetts", Abbreviation="MA" },
				new StateItem{ Name="Michigan", Abbreviation="MI" },
				new StateItem{ Name="Minnesota", Abbreviation="MN" },
				new StateItem{ Name="Mississippi", Abbreviation="MS" },
				new StateItem{ Name="Missouri", Abbreviation="MO" },
				new StateItem{ Name="Montana", Abbreviation="MT" },
				new StateItem{ Name="Nebraska", Abbreviation="NE" },
				new StateItem{ Name="Nevada", Abbreviation="NV" },
				new StateItem{ Name="New Hampshire", Abbreviation="NH" },
				new StateItem{ Name="New Jersey", Abbreviation="NJ" },
				new StateItem{ Name="New Mexico", Abbreviation="NM" },
				new StateItem{ Name="New York", Abbreviation="NY" },
				new StateItem{ Name="North Carolina", Abbreviation="NC" },
				new StateItem{ Name="North Dakota", Abbreviation="ND" },
				new StateItem{ Name="Ohio", Abbreviation="OH" },
				new StateItem{ Name="Oklahoma", Abbreviation="OK" },
				new StateItem{ Name="Oregon", Abbreviation="OR" },
				new StateItem{ Name="Pennsylvania", Abbreviation="PA" },
				new StateItem{ Name="Rhode Island", Abbreviation="RI" },
				new StateItem{ Name="South Carolina", Abbreviation="SC" },
				new StateItem{ Name="South Dakota", Abbreviation="SD" },
				new StateItem{ Name="Tennessee", Abbreviation="TN" },
				new StateItem{ Name="Texas", Abbreviation="TX" },
				new StateItem{ Name="Utah", Abbreviation="UT" },
				new StateItem{ Name="Vermont", Abbreviation="VT" },
				new StateItem{ Name="Virginia", Abbreviation="VA" },
				new StateItem{ Name="Washington", Abbreviation="WA" },
				new StateItem{ Name="West Virginia", Abbreviation="WV" },
				new StateItem{ Name="Wisconsin", Abbreviation="WI" },
				new StateItem{ Name="Wyoming", Abbreviation="WY" },
				new StateItem{ Name="District of Columbia", Abbreviation="DC" },
				new StateItem{ Name="American Samoa", Abbreviation="AS" },
				new StateItem{ Name="Guam", Abbreviation="GU" },
				new StateItem{ Name="Northern Mariana Islands", Abbreviation="MP" },
				new StateItem{ Name="Puerto Rico", Abbreviation="PR" },
				new StateItem{ Name="U.S. Virgin Islands", Abbreviation="VI" }
			};
			return result;
		}
		public string Name { get; set; }
		public string Abbreviation { get; set; }
		public override string ToString()
		{
			return this.Abbreviation;
		}
	}
}
