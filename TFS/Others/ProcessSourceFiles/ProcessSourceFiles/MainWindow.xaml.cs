using MVVMFramework;
using GregOsborne.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
namespace ProcessSourceFiles
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			if (DesignerProperties.GetIsInDesignMode(this))
				return;
			Left = Settings.GetValue<double>(App.ApplicationName, View.SectionName, "MainWindowLeft", 0.0);
			Top = Settings.GetValue<double>(App.ApplicationName, View.SectionName, "MainWindowTop", 0.0);
			Width = Settings.GetValue<double>(App.ApplicationName, View.SectionName, "MainWindowWidth", 400.0);
			Height = Settings.GetValue<double>(App.ApplicationName, View.SectionName, "MainWindowHeight", 350.0);
		}
		private void MainWindowView_CheckAccess(object sender, CheckAccessEventArgs e)
		{
			e.HasAccess = Dispatcher.CheckAccess();
			e.Dispatcher = Dispatcher;
		}
		private void MainWindowView_ExecuteUIAction(object sender, MVVMFramework.ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "ShowRulesWindow":
					var rulesWin = new RulesWindow
					{
						WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
						Owner = this
					};
					var rulesResult = rulesWin.ShowDialog();
					if (!rulesResult.GetValueOrDefault())
						return;
					break;
				case "DisplayFindWindow":
					var findWin = new FindSourceFilesWindow
					{
						WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
						Owner = this
					};
					findWin.View.TopFolder = View.LastFolder;
					var findResult = findWin.ShowDialog();
					if (!findResult.GetValueOrDefault())
						return;
					View.LastFolder = findWin.View.TopFolder;
					findWin.View.FileNames.OrderBy(x => System.IO.Path.GetFileName(x)).ToList().ForEach(fileName => View.OpenSourceFile(fileName));
					if (View.FileErrors.Count > 0)
						View.FileErrorVisibility = Visibility.Visible;
					break;
				case "ExitApplication":
					Application.Current.Shutdown(0);
					break;
				case "OpenSourceFile":
					var dlg = new Ookii.Dialogs.Wpf.VistaOpenFileDialog
					{
						AddExtension = false,
						CheckFileExists = true,
						CheckPathExists = true,
						Filter = "Project files|*.csproj|All source files|*.cs",
						FilterIndex = 1,
						InitialDirectory = string.IsNullOrEmpty(View.LastFolder) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : View.LastFolder,
						Multiselect = true,
						RestoreDirectory = false,
						ShowReadOnly = false,
						Title = "Open source file(s)..."
					};
					var dialogResult = dlg.ShowDialog(this);
					if (!dialogResult.GetValueOrDefault())
						return;
					View.LastFolder = System.IO.Path.GetDirectoryName(dlg.FileNames[0]);
					dlg.FileNames.ToList().ForEach(fileName => View.OpenSourceFile(fileName));
					if (View.FileErrors.Count > 0)
						View.FileErrorVisibility = Visibility.Visible;
					break;
			}
		}
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Settings.SetValue<double>(App.ApplicationName, View.SectionName, "MainWindowLeft", RestoreBounds.Left);
			Settings.SetValue<double>(App.ApplicationName, View.SectionName, "MainWindowTop", RestoreBounds.Top);
			Settings.SetValue<double>(App.ApplicationName, View.SectionName, "MainWindowWidth", RestoreBounds.Width);
			Settings.SetValue<double>(App.ApplicationName, View.SectionName, "MainWindowHeight", RestoreBounds.Height);
		}
		public MainWindowView View
		{
			get { return LayoutRoot.GetView<MainWindowView>(); }
		}
	}
}
