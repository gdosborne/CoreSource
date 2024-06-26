namespace OSInstaller.Windows
{
	using Microsoft.WindowsAPICodePack.Dialogs;
	using GregOsborne.MVVMFramework;
	using GregOsborne.Application.Windows;
	using OSInstallerCommands;
	using OSInstallerExtensibility.Events;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Interop;

	public partial class MainWindow : Window
	{
		#region Public Constructors
		public MainWindow()
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
		}
		#endregion Protected Methods

		#region Private Methods
		private static void onCurrentStepChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (MainWindow)source;
			if (src == null)
				return;
			var value = (IInstallerWizardStep)e.NewValue;
			src.StepBorder.Child = (UIElement)value;
		}
		private void MainWindowView_ExecuteAction(object sender, ExecuteUiActionEventArgs e)
		{
			if (e.CommandToExecute.Equals("CloseCancel"))
			{
				if (!(bool)e.Parameters["PromptFirst"])
					App.Current.Shutdown(0);
				else
				{
					var exitDlg = App.GetTaskDialog(
						View.Manager.ExpandVariable("CancelInstallTitle"),
						View.Manager.ExpandVariable("CancelInstallMessage"),
						View.Manager.ExpandVariable("CancelInstallAdditionalMessage"),
						string.Empty, TaskDialogStandardIcon.Error, new WindowInteropHelper(this).Handle,
						TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No);
					var exitDlgResult = exitDlg.Show();
					if (exitDlgResult == TaskDialogResult.Yes)
						App.Current.Shutdown(0);
				}
			}
		}
		private void MainWindowView_InstallationStarted(object sender, EventArgs e)
		{
			if (Dispatcher.CheckAccess())
			{
			}
			else
				Dispatcher.BeginInvoke(new EventHandler(MainWindowView_InstallationStarted), new object[] { sender, e });
		}
		private void MainWindowView_InstallComplete(object sender, InstallCompleteEventArgs e)
		{
			if (Dispatcher.CheckAccess())
			{
				View.Manager.Status = e.Status;
				if (e.Status == CommandStatuses.Failure)
				{
					View.Manager.GetCompleteStep().ModifyCompleteMessage("An error has occurred during installation. See the installation log for details.");
					View.Manager.DisplayStep(View.Manager.GetRevertStep());
				}
				else
					View.Manager.GoToNextStep();
			}
			else
				Dispatcher.BeginInvoke(new InstallCompleteHandler(MainWindowView_InstallComplete), new object[] { sender, e });
		}
		private void MainWindowView_InstallerCommandExecuting(object sender, InstallerCommandExecutingEventArgs e)
		{
			if (Dispatcher.CheckAccess())
			{
				var installingStep = View.Manager.Steps.FirstOrDefault(x => x.IsInstallationStep);
				if (installingStep != null && installingStep is IInstallerView)
				{
					((IInstallerView)installingStep).ProgressMaximum = e.PrimaryProgressBarMaximum + 1;
					((IInstallerView)installingStep).ProgressValue = e.PrimaryProgressBarValue + 1;
					((IInstallerView)installingStep).ProgressMessage = e.Message;
				}
			}
			else
				Dispatcher.BeginInvoke(new InstallerCommandExecutingHandler(MainWindowView_InstallerCommandExecuting), new object[] { sender, e });
		}
		private void MainWindowView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("Title"))
				Title = View.Title;
			else if (e.PropertyName.Equals("CurrentWizardStep"))
				CurrentStep = View.CurrentWizardStep;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (View != null)
				View.Initialize();
		}
		#endregion Private Methods

		#region Public Fields
		public static readonly DependencyProperty CurrentStepProperty = DependencyProperty.Register("CurrentStep", typeof(IInstallerWizardStep), typeof(MainWindow), new PropertyMetadata(null, onCurrentStepChanged));
		#endregion Public Fields

		#region Public Properties
		public IInstallerWizardStep CurrentStep
		{
			get { return (IInstallerWizardStep)GetValue(CurrentStepProperty); }
			set { SetValue(CurrentStepProperty, value); }
		}
		public MainWindowView View
		{
			get { return LayoutRoot.GetView<MainWindowView>(); }
		}
		#endregion Public Properties
	}
}
