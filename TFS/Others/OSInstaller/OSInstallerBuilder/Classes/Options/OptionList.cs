namespace OSInstallerBuilder.Classes.Options
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class OptionList : List<Category>
	{
		public Item GetItem(string categoryName, string pageName, string groupName, string itemName)
		{
			var ct = this.FirstOrDefault(x => x.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
			if (ct == null)
				return null;
			return ct.GetItem(pageName, groupName, itemName);
		}
	}
}
