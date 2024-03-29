namespace OSInstallerBuilder.Classes.Options
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	public class Category : NamedItem, INotifyPropertyChanged
	{
		public Category(string name)
		{
			Pages = new List<Page>();
			Name = name;
		}

		public Item GetItem(string pageName, string groupName, string itemName)
		{
			var pg = Pages.FirstOrDefault(x => x.Name.Equals(pageName, StringComparison.OrdinalIgnoreCase));
			if (pg == null)
				return null;
			return pg.GetItem(groupName, itemName);
		}

		#region Public Events
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private IList<Page> _Pages;
		#endregion Private Fields

		#region Public Properties
		public IList<Page> Pages
		{
			get
			{
				return _Pages;
			}
			private set
			{
				_Pages = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Pages"));
			}
		}
		#endregion Public Properties
	}
}
