namespace SDFManager
{
	using goApp = GregOsborne.Application;
	using GregOsborne.Application.Primitives;
	using MVVMFramework;
	using SDFManager.Settings;
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Linq;
	using SDFManagerControls;

	public class SettingsWindowView : INotifyPropertyChanged
	{
		#region Public Constructors
		public SettingsWindowView()
		{
			SearchTipVisibility = Visibility.Visible;
		}
		#endregion

		#region Public Methods
		public void Initialize(Window window)
		{
			window.Left = goApp.Settings.GetValue<double>(App.ApplicationName, "SettingsWindow", "Left", window.Left);
			window.Top = goApp.Settings.GetValue<double>(App.ApplicationName, "SettingsWindow", "Top", window.Top);
			window.Width = goApp.Settings.GetValue<double>(App.ApplicationName, "SettingsWindow", "Width", window.Width);
			window.Height = goApp.Settings.GetValue<double>(App.ApplicationName, "SettingsWindow", "Height", window.Height);
		}
		public void InitView()
		{
			Categories = CategoryItem.ApplicationCategories;
		}
		public void Persist(Window window)
		{
			goApp.Settings.SetValue<double>(App.ApplicationName, "SettingsWindow", "Left", window.RestoreBounds.Left);
			goApp.Settings.SetValue<double>(App.ApplicationName, "SettingsWindow", "Top", window.RestoreBounds.Top);
			goApp.Settings.SetValue<double>(App.ApplicationName, "SettingsWindow", "Width", window.RestoreBounds.Width);
			goApp.Settings.SetValue<double>(App.ApplicationName, "SettingsWindow", "Height", window.RestoreBounds.Height);
		}
		public void UpdateInterface()
		{
			SearchTipVisibility = string.IsNullOrEmpty(SearchText) ? Visibility.Visible : Visibility.Hidden;
		}
		#endregion

		#region Private Methods
		private void Cancel(object state)
		{
			DialogResult = false;
		}
		private void OK(object state)
		{
			var catPath = string.Empty;
			Categories.ToList().ForEach(x =>
			{
				ProcessCategory(x);
			});
			DialogResult = true;
		}
		private void ProcessCategory(CategoryItem cat)
		{
			cat.Items.ToList().ForEach(x =>
			{
				var keyName = x.Name;
				Application.Current.Resources[keyName] = x.Value.As<SolidColorBrush>();
				goApp.Settings.SetValue<object>(App.ApplicationName, "Settings", keyName, x.Value);
			});
			if (cat.Categories.Any())
			{
				cat.Categories.ToList().ForEach(x =>
				{
					ProcessCategory(x);
				});
			}
		}
		private void Search(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("GoToSearch"));
		}
		private bool ValidateCancelState(object state)
		{
			return true;
		}
		private bool ValidateOKState(object state)
		{
			return true;
		}
		private bool ValidateSearchState(object state)
		{
			return true;
		}
		#endregion

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Private Fields
		private DelegateCommand _CancelCommand = null;
		private ObservableCollection<CategoryItem> _Categories;
		private bool? _DialogResult;
		private DelegateCommand _OKCommand = null;
		private DelegateCommand _SearchCommand = null;
		private string _SearchText;
		private Visibility _SearchTipVisibility;
		private CategoryItem _SelectedCategory;
		#endregion

		#region Public Properties
		public DelegateCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		public ObservableCollection<CategoryItem> Categories
		{
			get { return _Categories; }
			set
			{
				_Categories = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool? DialogResult
		{
			get
			{
				return _DialogResult;
			}
			set
			{
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public DelegateCommand OKCommand
		{
			get
			{
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		public DelegateCommand SearchCommand
		{
			get
			{
				if (_SearchCommand == null)
					_SearchCommand = new DelegateCommand(Search, ValidateSearchState);
				return _SearchCommand as DelegateCommand;
			}
		}
		public string SearchText
		{
			get
			{
				return _SearchText;
			}
			set
			{
				_SearchText = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Visibility SearchTipVisibility
		{
			get
			{
				return _SearchTipVisibility;
			}
			set
			{
				_SearchTipVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		private FrameworkElement _SettingsElement;
		public FrameworkElement SettingsElement
		{
			get { return _SettingsElement; }
			set
			{
				_SettingsElement = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public CategoryItem SelectedCategory
		{
			get { return _SelectedCategory; }
			set
			{
				SettingsElement = null;
				_SelectedCategory = value;
				if (value == null)
					return;
				var items = new ObservableCollection<SettingItem>();
				if (value.Items.Count == 0)
				{
					if (value.Categories.Count == 0)
						return;
					foreach (var item in value.Categories)
					{
						if (item.Items.Count > 0)
						{
							SelectedCategory = item;
							return;
						}
					}
				}
				else
					items = value.Items;
				if (items != null)
				{
					SettingsElement = new GroupBox { Header = value.Name };
					var g = new Grid();
					items.ToList().ForEach(x =>
					{
						FrameworkElement thisElement = null;
						if (x.Value.Is<SolidColorBrush>())
						{
							thisElement = new SolidColorBrushEditor
							{
								Label = x.Name,
								ValueBrush = (Brush)x.Value,
								Tag = x
							};
							thisElement.As<SolidColorBrushEditor>().BrushChanged += SettingsWindowView_BrushChanged;
						}
						else if (x.Value.Is<LinearGradientBrush>())
						{
							thisElement = new LinearGradientBrushEditor
							{
								Label = x.Name,
								ValueBrush = (Brush)x.Value,
								Orientation = x.Value.As<LinearGradientBrush>().StartPoint.Y < x.Value.As<LinearGradientBrush>().EndPoint.Y ? Orientation.Vertical : Orientation.Horizontal,
								Tag = x
							};
						}
						else if (x.Value is bool)
						{
							thisElement = new CheckBox
							{
								Content = x.Name,
								IsChecked = (bool)x.Value,
								Tag = x,
								Margin = new Thickness(0, 2, 0, 2)
							};
						}
						if (thisElement != null)
						{
							g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
							thisElement.SetValue(Grid.RowProperty, g.RowDefinitions.Count - 1);
							g.Children.Add(thisElement);
						}
					});
					SettingsElement.As<GroupBox>().Content = g;
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}

		void SettingsWindowView_BrushChanged(object sender, BrushChangedEventArgs e)
		{
			var setting = sender.As<SolidColorBrushEditor>().Tag.As<SettingItem>();
			setting.Value = e.Brush;
		}
		#endregion
	}
}
