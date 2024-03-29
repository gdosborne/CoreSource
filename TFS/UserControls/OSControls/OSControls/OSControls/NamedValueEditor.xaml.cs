namespace OSControls
{
	using OSControls.ChildWindows;
	using OSControls.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;

	public partial class NamedValueEditor : UserControl
	{
		#region Public Constructors
		public NamedValueEditor()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Private Methods
		private static void onAlternatingBackgroundChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NamedValueEditor)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.TheDataGrid.AlternatingRowBackground = value;
		}
		private static void onBackgroundChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NamedValueEditor)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.TheDataGrid.Background = value;
		}
		private static void onIsEditableVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NamedValueEditor)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.TheDataGrid.Columns[3].Visibility = value;
		}
		private static void onIsRequiredVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NamedValueEditor)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.TheDataGrid.Columns[2].Visibility = value;
		}
		private static void onIsValidatedVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NamedValueEditor)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.TheDataGrid.Columns[3].Visibility = value;
		}
		private static void onItemsSourceChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NamedValueEditor)source;
			if (src == null)
				return;
			var value = (IEnumerable<object>)e.NewValue;
			src.TheDataGrid.ItemsSource = value;
		}
		private static void onRequiredNamesChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NamedValueEditor)source;
			if (src == null)
				return;
			var value = (IList<string>)e.NewValue;
		}
		private static void onVariableTriggerChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NamedValueEditor)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
		}
		private void AddMenuItem_Click(object sender, RoutedEventArgs e)
		{
			if (QueryAddNewItem != null)
			{
				var args = new QueryAddNewItemEventArgs();
				QueryAddNewItem(this, args);
				TheDataGrid.SelectedItem = args.TheNewItem;
			}
		}
		private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
		{
		}
		private void EditValue_Click(object sender, RoutedEventArgs e)
		{
			var editWin = new ValueEditor
			{
				Owner = Window.GetWindow(this)
			};
			var data = TheDataGrid.SelectedItem;
			var currentName = ((string)data.GetType().GetProperty("Name").GetValue(data));
			var names = new List<string>();
			var values = new List<string>();
			editWin.View.TheValue = ((string)data.GetType().GetProperty("Value").GetValue(data));
			for (int i = 0; i < TheDataGrid.Items.Count; i++)
			{
				var item = TheDataGrid.Items[i];
				var name = ((string)item.GetType().GetProperty("Name").GetValue(item));
				if (name.Equals(currentName))
					continue;
				var value = ((string)item.GetType().GetProperty("Value").GetValue(item));
				names.Add(name);
				values.Add(value);
			}
			editWin.View.AllVariableNames = names;
			editWin.View.AllVariableValues = values;
			editWin.View.VariableTrigger = VariableTrigger;
			var result = editWin.ShowDialog();
			if (!result.GetValueOrDefault())
				return;
			data.GetType().GetProperty("Value").SetValue(data, editWin.View.TheValue);
			var meth = data.GetType().GetMethod("EndEdit");
			if (meth != null)
				data.GetType().GetMethod("EndEdit").Invoke(data, null);
		}
		private void TheDataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
		{
			DeleteMenuItem.IsEnabled = TheDataGrid.SelectedItem != null && !_SelectedRowIsRequired;
		}
		private void TheDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if ((sender as DataGrid).SelectedItem == null)
				return;
			var nameCol = (sender as DataGrid).Columns[0];
			var valueCol = (sender as DataGrid).Columns[1];
			var data = (sender as DataGrid).SelectedItem;
			var itemName = ((string)data.GetType().GetProperty("Name").GetValue(data));
			var isEditProp = data.GetType().GetProperty("IsEditable");
			if (isEditProp != null)
				_SelectedRowIsEditable = (bool)isEditProp.GetValue(data);
			_SelectedRowIsRequired = RequiredNames.Contains(itemName);
			nameCol.IsReadOnly = _SelectedRowIsRequired;
			valueCol.IsReadOnly = !_SelectedRowIsEditable;
		}
		#endregion Private Methods

		#region Public Events
		public event QueryAddNewItemHandler QueryAddNewItem;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty AlternatingBackgroundProperty = DependencyProperty.Register("AlternatingBackground", typeof(Brush), typeof(NamedValueEditor), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(30, 0, 0, 255)), onAlternatingBackgroundChanged));
		public new static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(NamedValueEditor), new PropertyMetadata(new SolidColorBrush(Colors.White), onBackgroundChanged));
		public static readonly DependencyProperty IsEditableVisibilityProperty = DependencyProperty.Register("IsEditableVisibility", typeof(Visibility), typeof(NamedValueEditor), new PropertyMetadata(Visibility.Collapsed, onIsEditableVisibilityChanged));
		public static readonly DependencyProperty IsRequiredVisibilityProperty = DependencyProperty.Register("IsRequiredVisibility", typeof(Visibility), typeof(NamedValueEditor), new PropertyMetadata(Visibility.Collapsed, onIsRequiredVisibilityChanged));
		public static readonly DependencyProperty IsValidatedVisibilityProperty = DependencyProperty.Register("IsValidatedVisibility", typeof(Visibility), typeof(NamedValueEditor), new PropertyMetadata(Visibility.Collapsed, onIsValidatedVisibilityChanged));
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(NamedValueEditor), new PropertyMetadata(null, onItemsSourceChanged));
		public static readonly DependencyProperty RequiredNamesProperty = DependencyProperty.Register("RequiredNames", typeof(IList<string>), typeof(NamedValueEditor), new PropertyMetadata(null, onRequiredNamesChanged));
		public static readonly DependencyProperty VariableTriggerProperty = DependencyProperty.Register("VariableTrigger", typeof(string), typeof(NamedValueEditor), new PropertyMetadata("$", onVariableTriggerChanged));
		#endregion Public Fields

		#region Private Fields
		private bool _SelectedRowIsEditable = false;
		private bool _SelectedRowIsRequired = false;
		#endregion Private Fields

		#region Public Properties
		public Brush AlternatingBackground
		{
			get { return (Brush)GetValue(AlternatingBackgroundProperty); }
			set { SetValue(AlternatingBackgroundProperty, value); }
		}
		public new Brush Background
		{
			get { return (Brush)GetValue(BackgroundProperty); }
			set { SetValue(BackgroundProperty, value); }
		}
		public Visibility IsEditableVisibility
		{
			get { return (Visibility)GetValue(IsEditableVisibilityProperty); }
			set { SetValue(IsEditableVisibilityProperty, value); }
		}
		public Visibility IsRequiredVisibility
		{
			get { return (Visibility)GetValue(IsRequiredVisibilityProperty); }
			set { SetValue(IsRequiredVisibilityProperty, value); }
		}
		public Visibility IsValidatedVisibility
		{
			get { return (Visibility)GetValue(IsValidatedVisibilityProperty); }
			set { SetValue(IsValidatedVisibilityProperty, value); }
		}
		public IEnumerable<object> ItemsSource
		{
			get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}
		public IList<string> RequiredNames
		{
			get { return (IList<string>)GetValue(RequiredNamesProperty); }
			set { SetValue(RequiredNamesProperty, value); }
		}
		public string VariableTrigger
		{
			get { return (string)GetValue(VariableTriggerProperty); }
			set { SetValue(VariableTriggerProperty, value); }
		}
		#endregion Public Properties
	}
}
