namespace OSInstallerBuilder.Classes.Options
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;

	public class Page : NamedItem, INotifyPropertyChanged
	{
		#region Public Constructors
		public Page(string name, Category category)
		{
			Groups = new List<Group>();
			Category = category;
			Name = name;
		}
		#endregion Public Constructors

		public Item GetItem(string groupName, string itemName)
		{
			var gp = Groups.FirstOrDefault(x => x.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));
			if (gp == null)
				return null;
			return gp.GetItem(itemName);
		}
		private Category _Category;
		public Category Category
		{
			get { return _Category; }
			private set
			{
				_Category = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Category"));
			}
		}

		#region Public Methods
		public Grid GetControl()
		{
			var result = new Grid();
			Groups.ToList().ForEach(x =>
			{
				result.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
				var ctrl = x.GetControl();
				ctrl.SetValue(Grid.RowProperty, result.RowDefinitions.Count - 1);
				result.Children.Add(ctrl);
			});
			return result;
		}
		#endregion Public Methods

		#region Public Events
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private IList<Group> _Groups;
		#endregion Private Fields

		#region Public Properties
		public IList<Group> Groups
		{
			get
			{
				return _Groups;
			}
			private set
			{
				_Groups = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Groups"));
			}
		}
		#endregion Public Properties
	}
}
