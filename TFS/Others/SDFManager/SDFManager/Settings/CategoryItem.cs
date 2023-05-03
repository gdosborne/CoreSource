namespace SDFManager.Settings
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Windows.Media;

	public class CategoryItem
	{
		#region Public Constructors
		public CategoryItem()
		{
			Items = new ObservableCollection<SettingItem>();
			Categories = new ObservableCollection<CategoryItem>();
		}
		#endregion

		#region Public Properties
		public ObservableCollection<SettingItem> Items { get; private set; }
		public string Name { get; set; }
		#endregion

		public ObservableCollection<CategoryItem> Categories { get; private set; }

		public static ObservableCollection<CategoryItem> ApplicationCategories = new ObservableCollection<CategoryItem>
		{
			new CategoryItem
			{
				Name = "Visual",
				Categories = new ObservableCollection<CategoryItem>
				{
					new CategoryItem
					{
						Name = "Theme",
						Items = new ObservableCollection<SettingItem>
						{
							new SettingItem { Name = "Visual.Theme.Window.Background", Value = (Brush)SDFManager.App.Current.Resources["Visual.Theme.Window.Background"] },
							new SettingItem { Name = "Visual.Theme.Text", Value = (Brush)SDFManager.App.Current.Resources["Visual.Theme.Text"] },
							new SettingItem { Name = "Visual.Theme.Control.Border", Value = (Brush)SDFManager.App.Current.Resources["Visual.Theme.Control.Border"] },
							new SettingItem { Name = "Visual.Theme.Table.Background", Value = (Brush)SDFManager.App.Current.Resources["Visual.Theme.Table.Background"] },
							new SettingItem { Name = "Visual.Theme.Table.Foreground", Value = (Brush)SDFManager.App.Current.Resources["Visual.Theme.Table.Foreground"] },
							new SettingItem { Name = "Visual.Theme.Table.Border", Value = (Brush)SDFManager.App.Current.Resources["Visual.Theme.Table.Border"] },
							new SettingItem { Name = "Visual.Theme.Table.Primary.Key.Background", Value = (Brush)SDFManager.App.Current.Resources["Visual.Theme.Table.Primary.Key.Background"] },
							new SettingItem { Name = "Visual.Theme.Table.Primary.Key.Foreground", Value = (Brush)SDFManager.App.Current.Resources["Visual.Theme.Table.Primary.Key.Foreground"] },
							new SettingItem { Name = "Visual.Theme.Table.Header.Background", Value = (Brush)SDFManager.App.Current.Resources["Visual.Theme.Table.Header.Background"] },
							new SettingItem { Name = "Visual.Theme.Table.Header.Foreground", Value = (Brush)SDFManager.App.Current.Resources["Visual.Theme.Table.Header.Foreground"] }
						}
					}
				}
			},
			new CategoryItem
			{
				Name = "Startup",
				Items = new ObservableCollection<SettingItem>
				{
					new SettingItem { Name = "Remember.Last.Database.Folder", Value = true },
					new SettingItem { Name = "Remember.Last.Scale.Value", Value = true }
				}
			}
		};
	}
}
