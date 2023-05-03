namespace OSInstallerBuilder.Windows
{
	using MVVMFramework;
	using MyApplication;
	using MyApplication.Primitives;
	using MyApplication.Windows;
	using OSInstallerBuilder.Classes.Options;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using MyApplication.Windows.Controls;
	using MyApplication.Media;

	public partial class OptionsWindow : Window
	{
		#region Public Constructors
		public OptionsWindow()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e)
		{
			this.HideMinimizeAndMaximizeButtons();
		}
		#endregion Protected Methods

		#region Private Methods
		private void OptionsWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
		{
			if (e.CommandToExecute.Equals("LoadOptionGroups"))
			{
				var page = e.Parameters["Page"].As<OSInstallerBuilder.Classes.Options.Page>();
			}
		}
		private void OptionsWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("DialogResult"))
				this.DialogResult = View.DialogResult;
			else if (e.PropertyName.Equals("OptionCategories"))
			{
				var cat = View.OptionCategories.FirstOrDefault(x => x.Name.Equals("General"));
				if (cat == null)
					return;
				cat.IsExpanded = true;
				var pg = cat.Pages.FirstOrDefault(x => x.Name.Equals("General"));
				if (pg == null)
					return;
				pg.IsSelected = true;
				MyTreeView.Focus();
			}
		}
		private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			View.SelectedTreeItem = e.NewValue.As<NamedItem>();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Settings.SetValue<double>(App.ApplicationName, "OptionsWindow", "Left", RestoreBounds.Left);
			Settings.SetValue<double>(App.ApplicationName, "OptionsWindow", "Top", RestoreBounds.Top);
			Settings.SetValue<double>(App.ApplicationName, "OptionsWindow", "Width", RestoreBounds.Width);
			Settings.SetValue<double>(App.ApplicationName, "OptionsWindow", "Height", RestoreBounds.Height);
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var item = View.OptionCategories.GetItem("General", "General", "Startup", "Save Window Position");
			if (item != null && (bool)item.Value)
			{
				Left = Settings.GetValue<double>(App.ApplicationName, "OptionsWindow", "Left", Left);
				Top = Settings.GetValue<double>(App.ApplicationName, "OptionsWindow", "Top", Top);
				Width = Settings.GetValue<double>(App.ApplicationName, "OptionsWindow", "Width", Width);
				Height = Settings.GetValue<double>(App.ApplicationName, "OptionsWindow", "Height", Height);
			}
		}
		#endregion Private Methods

		#region Public Properties
		public OptionsWindowView View
		{
			get { return LayoutRoot.GetView<OptionsWindowView>(); }
		}
		#endregion Public Properties

	}
}
