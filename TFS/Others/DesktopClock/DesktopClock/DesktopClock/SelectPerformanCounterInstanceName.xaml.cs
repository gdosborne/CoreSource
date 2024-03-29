namespace DesktopClock
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;

	public partial class SelectPerformanCounterInstanceName : Window
	{
		#region Public Constructors
		public SelectPerformanCounterInstanceName()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Private Methods
		private void SelectPerformanCounterInstanceNameView_ExecuteUIAction(object sender, MVVMFramework.ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "AddRadioButton":
					MyWrapPanel.Children.Add(e.Parameters["value"] as RadioButton);
					break;
				case "CloseDialog":
					DialogResult = (bool)e.Parameters["result"];
					break;
			}
		}
		private void SelectPerformanCounterInstanceNameView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			View.OKCommand.RaiseCanExecuteChanged();
		}
		#endregion Private Methods

		#region Public Properties
		public SelectPerformanCounterInstanceNameView View
		{
			get
			{
				if (DesignerProperties.GetIsInDesignMode(this))
					return new SelectPerformanCounterInstanceNameView();
				return LayoutRoot.DataContext as SelectPerformanCounterInstanceNameView;
			}
		}
		#endregion Public Properties
	}
}
