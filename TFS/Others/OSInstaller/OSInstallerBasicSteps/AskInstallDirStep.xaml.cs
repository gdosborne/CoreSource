namespace OSInstallerBasicSteps
{
	using Microsoft.WindowsAPICodePack.Dialogs;
	using GregOsborne.MVVMFramework;
	using GregOsborne.Application.Primitives;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;

	public partial class AskInstallDirStep : UserControl, IInstallerWizardStep
	{
		#region Public Constructors
		public AskInstallDirStep()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Public Methods
		public void Initialize()
		{
		}
		#endregion Public Methods

		#region Private Methods
		private void AskInstallDirStepView_ExecuteUIAction(object sender, GregOsborne.MVVMFramework.ExecuteUiActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "SelectInstallationDirectory":
					var dialog = new CommonOpenFileDialog
					{
						AddToMostRecentlyUsedList = false,
						AllowNonFileSystemItems = true,
						EnsureFileExists = true,
						EnsurePathExists = true,
						DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
						InitialDirectory = View.InstallationDirectory,
						Multiselect = false,
						Title = "Select installation directory...",
						IsFolderPicker = true
					};
					CommonFileDialogResult result = dialog.ShowDialog();
					if (result == CommonFileDialogResult.Cancel)
						return;
					Manager.Datum.First(x => x.Equals("InstallationDir")).Value = dialog.FileName;
					break;
			}
		}
		private void InstallDirTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			sender.As<TextBox>().SelectAll();
		}
		#endregion Private Methods

		#region Private Fields
		private IInstallerManager _Manager = null;
		#endregion Private Fields

		#region Public Properties
		public string Id { get; set; }
		public bool IsInstallationStep { get; set; }
		public IInstallerManager Manager
		{
			get
			{
				return _Manager;
			}
			set
			{
				_Manager = value;
				View.Header = Manager.ExpandVariable(Id, "Header");
				View.InstallationDirectory = Manager.ExpandVariable("InstallationDir");
				View.InstallationDirectoryPrompt = Manager.ExpandVariable(Id, "InstallationDirectoryPrompt");
				View.WindowText = new SolidColorBrush((Color)Manager.Properties.First(x => x.Name.Equals("WindowText")).Value);
			}
		}
		public int Sequence { get; set; }
		public AskInstallDirStepView View
		{
			get { return LayoutRoot.GetView<AskInstallDirStepView>(); }
		}
		#endregion Public Properties
	}
}
