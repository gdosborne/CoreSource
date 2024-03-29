// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// MainWindow
//
namespace MoMoney
{
	using MoMoney.Views;
	using MVVMFramework;
	using Ookii.Dialogs.Wpf;
	using System;
	using System.Windows;
	using System.Linq;
	using System.IO;

	public partial class MainWindow : Window
	{
		#region Public Constructors
		public MainWindow() {
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Private Methods
		private void DataGrid_BeginningEdit(object sender, System.Windows.Controls.DataGridBeginningEditEventArgs e) {
		}
		private void MainWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e) {
			var okButton = new TaskDialogButton(ButtonType.Ok);
			var yesButton = new TaskDialogButton(ButtonType.Yes);
			var noButton = new TaskDialogButton(ButtonType.No);
			var cancelButton = new TaskDialogButton(ButtonType.Cancel);
			switch (e.CommandToExecute) {
				case "OpenFile":
					var dlg1 = new VistaOpenFileDialog
					{
						AddExtension = true,
						CheckFileExists = true,
						CheckPathExists = true,
						DefaultExt = "xml",
						Filter = "MoMoney files|*.mmf",
						InitialDirectory = string.IsNullOrEmpty(Properties.Settings.Default.LastDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : Properties.Settings.Default.LastDirectory,
						Multiselect = false,
						Title = "Open data file..."
					};
					if (!dlg1.ShowDialog(this).GetValueOrDefault())
						return;
					View.CurrentFileName = dlg1.FileName;
					break;

				case "SaveFileAs":
					var dlg2 = new VistaSaveFileDialog
					{
						AddExtension = true,
						CheckFileExists = false,
						CheckPathExists = true,
						DefaultExt = "xml",
						Filter = "MoMoney files|*.mmf",
						InitialDirectory = string.IsNullOrEmpty(Properties.Settings.Default.LastDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : Properties.Settings.Default.LastDirectory,
						OverwritePrompt = true,
						Title = "Save data file as..."
					};
					if (!dlg2.ShowDialog(this).GetValueOrDefault())
						return;
					View.SaveFileAs(dlg2.FileName);
					break;
				case "ShowSettings":
					new SettingsWindow
					{
						WindowStartupLocation = WindowStartupLocation.CenterOwner,
						Owner = this
					}.ShowDialog();
					break;
				case "ShowFilePassword":
					var pwdWin = new PasswordWindow
					{
						WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
						Owner = this
					};
					pwdWin.View.FileName = MyApplication.NativeMethods.PathShortener(View.CurrentFileName, 40);
					var result = pwdWin.ShowDialog();
					if (!result.GetValueOrDefault())
						return;
					View.CurrentPassword = pwdWin.View.Password;
					if (View.IsSaveFile)
						View.SaveAsCommand.Execute(null);
					else
						View.OpenFile();
					break;
				case "ShowNewAccountWindow":
					var acctWin = new AccountWindow
					{
						WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
						Owner = this
					};
					var result1 = acctWin.ShowDialog();
					if (!result1.GetValueOrDefault())
						return;
					if (View.DataProvider.Accounts.Any(x => x.Name.Equals(acctWin.View.Name, StringComparison.OrdinalIgnoreCase))) {
						var td2 = new TaskDialog
						{
							AllowDialogCancellation = false,
							ButtonStyle = TaskDialogButtonStyle.Standard,
							CenterParent = true,
							MainIcon = TaskDialogIcon.Error,
							MainInstruction = string.Format("An account already exists named \"{0}\".", acctWin.View.Name),
							MinimizeBox = false,
							WindowTitle = "Error"
						};
						td2.Buttons.Add(okButton);
						td2.ShowDialog(this);
						return;
					}
					View.DataProvider.Accounts.Add(new Data.Account
					{
						AccountType = acctWin.View.SelectedAccountType,
						Created = DateTime.Now,
						ID = View.DataProvider.MaxAccountNumber + 1,
						Name = acctWin.View.Name,
						StartingBalance = acctWin.View.BeginningBalance
					});
					break;
				case "DisplayException":
					var td1 = new TaskDialog
					{
						AllowDialogCancellation = false,
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						MainIcon = TaskDialogIcon.Error,
						MainInstruction = (e.Parameters["exception"] as Exception).Message,
						MinimizeBox = false,
						WindowTitle = "Error"
					};
					td1.Buttons.Add(okButton);
					td1.ShowDialog(this);
					break;
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Properties.Settings.Default.WindowState = WindowState;
			Properties.Settings.Default.Left = RestoreBounds.Left;
			Properties.Settings.Default.Top = RestoreBounds.Top;
			Properties.Settings.Default.Width = RestoreBounds.Width;
			Properties.Settings.Default.Height = RestoreBounds.Height;
			Properties.Settings.Default.Save();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			Left = Properties.Settings.Default.Left;
			Top = Properties.Settings.Default.Top;
			Width = Properties.Settings.Default.Width;
			Height = Properties.Settings.Default.Height;
			WindowState = Properties.Settings.Default.WindowState;

			View.InitView();
		}
		#endregion Private Methods

		#region Public Properties
		public MainWindowView View {
			get {
				return LayoutRoot.GetView<MainWindowView>();
			}
		}
		#endregion Public Properties
	}
}
