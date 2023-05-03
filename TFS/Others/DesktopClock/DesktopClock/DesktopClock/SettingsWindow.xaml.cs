namespace DesktopClock
{
	using DesktopClock.Classes;
	using MVVMFramework;
	using MyApplication.Primitives;
	using MyApplication.Windows;
	using Ookii.Dialogs.Wpf;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;

	public partial class SettingsWindow : Window, INotifyPropertyChanged
	{
		#region Public Constructors
		public SettingsWindow()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
			View.BeginInitialization();
		}
		#endregion Protected Methods

		#region Private Methods
		private void CategoryTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			ItemsGrid.Children.Clear();
			ItemsGrid.RowDefinitions.Clear();
			View.SelectedTreeViewItem = e.NewValue.As<TreeViewItem>();
		}

		private void SettingsWindowView_AddControlToEditor(object sender, Classes.AddControlToEditorEventArgs e)
		{
			ItemsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
			e.Element.SetValue(Grid.RowProperty, ItemsGrid.RowDefinitions.Count - 1);
			ItemsGrid.Children.Add(e.Element);
		}

		private void SettingsWindowView_AddTopLevelSetting(object sender, Classes.AddTopLevelSettingEventArgs e)
		{
			CategoryTreeView.Items.Add(e.Item);
		}

		private void SettingsWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "Close":
					this.DialogResult = (bool)e.Parameters["result"];
					break;

				case "SelectClockFace":
					var dlg = new VistaOpenFileDialog
					{
						CheckFileExists = true,
						CheckPathExists = true,
						Filter = "Images|*.jpg;*.png;*.bmp",
						InitialDirectory = View.LastClockFaceDirectory,
						Multiselect = false,
						Title = "Select alternate clock face..."
					};
					if (!dlg.ShowDialog(this).GetValueOrDefault())
						return;
					View.LastClockFaceDirectory = System.IO.Path.GetDirectoryName(dlg.FileName);
					View.OtherClockFaceFileName = dlg.FileName;
					break;
			}
		}

		private void SettingsWindowView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Private Methods

		#region Public Events
		public event AddNewAnalogClockHandler AddNewAnalogClock;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Public Properties
		public SettingsWindowView View
		{
			get { return LayoutRoot.GetView<SettingsWindowView>(); }
		}
		#endregion Public Properties
	}
}
