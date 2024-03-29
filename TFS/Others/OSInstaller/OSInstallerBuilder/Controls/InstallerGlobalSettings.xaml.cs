namespace OSInstallerBuilder.Controls
{
	using Microsoft.WindowsAPICodePack.Dialogs;
	using GregOsborne.MVVMFramework;
	using GregOsborne.Application.Primitives;
	using OSInstallerCommands;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Interop;

	public partial class InstallerGlobalSettings : UserControl, IInstallerSettingsController
	{
		#region Public Constructors
		public InstallerGlobalSettings()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Public Methods
		public void Reset()
		{
			Manager = null;
			View.UpdateInterface();
		}
		#endregion Public Methods

		#region Private Methods
		private static void onManagerChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (InstallerGlobalSettings)source;
			if (src == null)
				return;
			var value = (IInstallerManager)e.NewValue;
			src.View.Manager = value;
		}
		private void InstallerGlobalSettingsView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (View.IsInitializing)
				return;
			if (e.PropertyName.Equals("Manager")) { }
			else
			{
				if (SettingsChanged != null)
					SettingsChanged(this, EventArgs.Empty);
				if (e.PropertyName.Equals("VariableTrigger"))
				{
					var td = App.GetTaskDialog(
						"Variable character",
						"Variable trigger character has changed.",
						"Change all variables in the installation to the new trigger character?",
						"If you do not change the character at this time, you must close and re-open the installation if you want to change the trigger character again.",
						TaskDialogStandardIcon.Information, (new WindowInteropHelper(Window.GetWindow(this))).Handle, TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No);
					var result = td.Show();
					if (result == TaskDialogResult.Yes)
					{
						View.Manager.Datum.ToList().ForEach(x =>
						{
							x.Value = x.Value.Replace(View.PreviousVariableTrigger, View.VariableTrigger);
						});
						ModifyCommands(View.Manager.Commands);
					}
					View.PreviousVariableTrigger = View.VariableTrigger;
				}
			}
		}
		private void ModifyCommands(IList<BaseCommand> commands)
		{
			commands.ToList().ForEach(x =>
			{
				x.Parameters.Keys.ToList().ForEach(y =>
				{
					if (x.Parameters[y].GetType() == typeof(string))
						x.Parameters[y] = (x.Parameters[y] as string).Replace(View.PreviousVariableTrigger, View.VariableTrigger);
					ModifyCommands(x.Commands);
				});
			});
		}
		private void TextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			sender.As<TextBox>().SelectAll();
		}
		private void UserControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (View != null)
				View.ControlEnabled = (bool)e.NewValue;
		}
		#endregion Private Methods

		#region Public Events
		public event EventHandler SettingsChanged;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty ManagerProperty = DependencyProperty.Register("Manager", typeof(IInstallerManager), typeof(InstallerGlobalSettings), new PropertyMetadata(null, onManagerChanged));
		#endregion Public Fields

		#region Public Properties
		public IInstallerManager Manager
		{
			get { return (IInstallerManager)GetValue(ManagerProperty); }
			set { SetValue(ManagerProperty, value); }
		}
		public InstallerGlobalSettingsView View
		{
			get { return LayoutRoot.GetView<InstallerGlobalSettingsView>(); }
		}
		#endregion Public Properties
	}
}
