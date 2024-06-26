namespace OSInstallerBuilder.Classes.Options {
	using GregOsborne.Application;
	using GregOsborne.MVVMFramework;
	using GregOsborne.Application.Media;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Data;
	using System.Windows.Media;
	using Xceed.Wpf.Toolkit;

	public class Item : NamedItem, INotifyPropertyChanged
	{
		#region Public Constructors
		public Item(string name, Group group, Type valueType, object defaultValue)
		{
			Name = name;
			ValueType = valueType;
			Value = defaultValue;
			Group = group;
			RegSection = string.Format(@"Options\{0}\{1}\{2}", group.Page.Category.Name, group.Page.Name, group.Name);
			StringSubType = StringSubTypes.None;
		}
		#endregion Public Constructors

		#region Public Methods
		public FrameworkElement GetControl()
		{
			FrameworkElement result = null;
			if (ValueType == typeof(bool))
				result = GetCheckBox(Name, (bool)Value);
			else if (ValueType == typeof(string))
				result = GetFileSystemSelectionGrid(Name, StringSubType, (string)Value);
			else if (ValueType == typeof(List<string>))
				result = GetStringListGrid((List<string>)Value);
			else if (ValueType == typeof(Color))
				result = GetColorGrid(Name, (Color)Value);
			return result;
		}
		public void UpdateValue()
		{
			//if (ValueType == typeof(bool))
			//{
			//	Settings.AddOrUpdateSetting(RegSection, Name, Value);
			//}
			//else if (ValueType == typeof(string))
			//{
			//	Settings.SetValue<string>(App.ApplicationName, RegSection, Name, (string)Value);
			//}
			//else if (ValueType == typeof(List<string>))
			//{
			//	Settings.SetValue<string>(App.ApplicationName, RegSection, Name, string.Join(",", (List<string>)Value));
			//}
			//else if (ValueType == typeof(Color))
			//{
			//	Settings.SetValue<string>(App.ApplicationName, RegSection, Name, ((Color)Value).ToHexValue());
			//}
		}
		#endregion Public Methods

		#region Private Methods
		private CheckBox GetCheckBox(string name, bool value)
		{
			var result = new CheckBox
			{
				Content = name,
				Margin = new Thickness(0, 2.5, 0, 2.5),
				IsChecked = value
			};
			var binding = new Binding("Value")
			{
				BindsDirectlyToSource=true,
				Source = this
			};
			result.SetBinding(CheckBox.IsCheckedProperty, binding);
			return result;
		}
		private Grid GetColorGrid(string name, Color value)
		{
			var grid = new Grid();
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			var lbl = new Label
			{
				Content = name,
				VerticalAlignment = VerticalAlignment.Center
			};
			lbl.SetValue(Grid.ColumnProperty, 0);
			grid.Children.Add(lbl);
			var picker = new ColorPicker
			{
				VerticalAlignment = VerticalAlignment.Center,
				SelectedColor = value
			};
			var binding = new Binding("Value")
			{
				BindsDirectlyToSource = true,
				Source = this
			};
			picker.SetBinding(ColorPicker.SelectedColorProperty, binding);
			picker.SetValue(Grid.ColumnProperty, 1);
			grid.Children.Add(picker);
			return grid;
		}
		private Grid GetFileSystemSelectionGrid(string name, StringSubTypes subType, string value)
		{
			var grid = new Grid();
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			if (subType == StringSubTypes.Directory || subType == StringSubTypes.File)
				grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
			var lbl = new Label
			{
				Content = name,
				VerticalAlignment = VerticalAlignment.Center
			};
			lbl.SetValue(Grid.ColumnProperty, 0);
			grid.Children.Add(lbl);
			var tb = new TextBox
			{
				Text = value,
				VerticalAlignment = VerticalAlignment.Center
			};
			var binding = new Binding("Value")
			{
				BindsDirectlyToSource = true,
				Source = this
			};
			tb.SetBinding(TextBox.TextProperty, binding);
			tb.SetValue(Grid.ColumnProperty, 1);
			grid.Children.Add(tb);
			if (subType == StringSubTypes.Directory || subType == StringSubTypes.File)
			{
				var btn = new Button
				{
					Content = "...",
					VerticalAlignment = VerticalAlignment.Center,
					Width = 18,
					Height = 18,
					Margin = new Thickness(5, 0, 0, 0),
				};
				btn.SetValue(Grid.ColumnProperty, 2);
				grid.Children.Add(btn);
			}
			return grid;
		}
		private Grid GetStringListGrid(List<string> value)
		{
			var grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			var lb = new ListBox
			{
				Height = 180,
				Margin = new Thickness(0, 5, 0, 5),
				ItemsSource = value
			};
			var binding = new Binding("Value")
			{
				BindsDirectlyToSource = true,
				Source = this
			};
			lb.SetBinding(ListBox.ItemsSourceProperty, binding);
			lb.SetValue(Grid.RowProperty, 0);
			grid.Children.Add(lb);
			var g = GetFileSystemSelectionGrid("Add new", StringSubTypes.Directory, string.Empty);
			g.SetValue(Grid.RowProperty, 1);
			grid.Children.Add(g);
			return grid;
		}
		#endregion Private Methods

		#region Public Events
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private Group _Group;
		private string _RegSection;
		private int _Sequence;
		private StringSubTypes _StringSubType;
		private object _Value;
		private Type _ValueType;
		#endregion Private Fields

		#region Public Enums
		public enum StringSubTypes
		{
			None,
			File,
			Directory
		}
		#endregion Public Enums

		#region Public Properties
		public Group Group
		{
			get
			{
				return _Group;
			}
			private set
			{
				_Group = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Group"));
			}
		}
		public string RegSection
		{
			get
			{
				return _RegSection;
			}
			set
			{
				_RegSection = value;
				//if (ValueType == typeof(bool))
				//{
				//	Value = Settings.GetValue<bool>(App.ApplicationName, _RegSection, Name, (bool)Value);
				//}
				//else if (ValueType == typeof(string))
				//{
				//	Value = Settings.GetValue<string>(App.ApplicationName, _RegSection, Name, (string)Value);
				//}
				//else if (ValueType == typeof(List<string>))
				//{
				//	var temp = string.Join(",", (List<string>)Value);
				//	Value = ((string)Settings.GetValue<string>(App.ApplicationName, _RegSection, Name, temp)).Split(',').ToList();
				//}
				//else if (ValueType == typeof(Color))
				//{
				//	Value = ((string)Settings.GetValue<string>(App.ApplicationName, _RegSection, Name, ((Color)Value).ToHexValue())).ToColor();
				//}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RegSection"));
			}
		}
		public int Sequence
		{
			get
			{
				return _Sequence;
			}
			set
			{
				_Sequence = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Sequence"));
			}
		}
		public StringSubTypes StringSubType
		{
			get
			{
				return _StringSubType;
			}
			set
			{
				_StringSubType = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("StringSubType"));
			}
		}
		public object Value
		{
			get
			{
				return _Value;
			}
			set
			{
				_Value = value;
				ValueType = _Value.GetType();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Value"));
			}
		}
		public Type ValueType
		{
			get
			{
				return _ValueType;
			}
			private set
			{
				_ValueType = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ValueType"));
			}
		}
		#endregion Public Properties
	}
}
