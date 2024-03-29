namespace OSInstallerBuilder.Classes.Options
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;

	public class Group : NamedItem, INotifyPropertyChanged
	{
		#region Public Constructors
		public Group(string name, Page page)
		{
			Items = new List<Item>();
			Page = page;
			Name = name;
		}
		#endregion Public Constructors
		public Item GetItem(string itemName)
		{
			return Items.FirstOrDefault(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
		}
		#region Public Methods
		private Page _Page;
		public Page Page
		{
			get { return _Page; }
			private set
			{
				_Page = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Page"));
			}
		}
		public GroupBox GetControl()
		{
			var result = new GroupBox
			{
				Header = Name
			};
			var grid = new Grid();
			Items.OrderBy(x => x.Sequence).ToList().ForEach(x =>
			{
				grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
				var ctrl = x.GetControl();
				ctrl.SetValue(Grid.RowProperty, grid.RowDefinitions.Count - 1);
				grid.Children.Add(ctrl);
			});
			result.Content = grid;
			return result;
		}
		#endregion Public Methods

		#region Public Events
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private IList<Item> _Items;
		#endregion Private Fields

		#region Public Properties
		public IList<Item> Items
		{
			get
			{
				return _Items;
			}
			private set
			{
				_Items = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Items"));
			}
		}
		#endregion Public Properties
	}
}
