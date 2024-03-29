namespace SDFManager
{
	using GregOsborne.Application.Windows;
	using MVVMFramework;
	using SDFManager.Settings;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

	public partial class SettingsWindow : Window
	{
		#region Public Constructors
		public SettingsWindow()
		{
			InitializeComponent();
			if (View != null)
			{
				View.ExecuteUIAction += View_ExecuteUIAction;
				View.PropertyChanged += View_PropertyChanged;
			}
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
		}
		#endregion Protected Methods

		#region Private Methods
		private void View_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "GoToSearch":
					SearchBox.Focus();
					break;
			}
		}
		private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "DialogResult":
					this.DialogResult = View.DialogResult;
					break;
				case "SettingsElement":
					if (View.SettingsElement != null)
						SettingsBorder.Child = View.SettingsElement;
					break;
			}
		}
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			View.Persist(this);
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			View.InitView();
			View.Initialize(this);
		}
		#endregion Private Methods

		#region Public Properties
		public SettingsWindowView View
		{
			get
			{
				return LayoutRoot.GetView<SettingsWindowView>();
			}
		}
		#endregion Public Properties

		private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			View.SelectedCategory = e.NewValue as CategoryItem;
		}
	}
}
