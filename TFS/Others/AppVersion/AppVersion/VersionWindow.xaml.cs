namespace GregOsborne.AppVersion
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Data;
	using System.Windows.Documents;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using System.Windows.Shapes;
	using GregOsborne.Application.Windows;
	using MVVMFramework;
	using Ookii.Dialogs.Wpf;
	using System.IO;

	public partial class VersionWindow : Window
	{
		public VersionWindow() {
			InitializeComponent();
		}

		public VersionWindowView View {
			get {
				return LayoutRoot.GetView<VersionWindowView>();
			}
		}

		protected override void OnSourceInitialized(EventArgs e) {
			base.OnSourceInitialized(e);
			this.HideControlBox();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			if (Properties.Settings.Default.RequiresUpdate) {
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.RequiresUpdate = false;
				Properties.Settings.Default.Save();
			}
			try {
				this.Activate();
				View.InitView();
			}
			catch (Exception ex) {
				View.ErrorVisibility = Visibility.Visible;
				View.ErrorText = ex.Message;
			}
		}

		private void VersionWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult":
					DialogResult = View.DialogResult;
					break;
			}
		}

		private void VersionWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e) {
			var yesButton = new TaskDialogButton(ButtonType.Yes);
			var noButton = new TaskDialogButton(ButtonType.No);
			VistaOpenFileDialog openDialog = null;
			string fileName = null;
			string dir = null;
			switch (e.CommandToExecute) {
				case "AskClearAlternateFile":
					var dlg1 = new TaskDialog
					{
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						MainIcon = TaskDialogIcon.Warning,
						MainInstruction = "You are abount to clear the alternate assmbly info file.\r\n\r\nContinue?"
					};
					dlg1.Buttons.Add(yesButton);
					dlg1.Buttons.Add(noButton);
					var result = dlg1.ShowDialog(this);
					if (result.ButtonType == ButtonType.No)
						return;
					View.ClearAltFile();
					this.Activate();
					break;
				case "SelectCPPVariableFileName":
					dir = string.IsNullOrEmpty(Properties.Settings.Default.LastDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : Properties.Settings.Default.LastDirectory;
					openDialog = new VistaOpenFileDialog
					{
						AddExtension = true,
						CheckFileExists = true,
						DefaultExt = "cpp",
						Filter = "All C++ files|*.cpp;*.h|All files|*.*",
						InitialDirectory = dir,
						Multiselect = false,
						Title = "Select file where version variable is located..."
					};
					if (!openDialog.ShowDialog(this).GetValueOrDefault())
						return;
					fileName = openDialog.FileName;

					var cppVarWin = new SetCPPVariablesWindow
					{
						Owner = this
					};
					cppVarWin.View.FileName = fileName;
					var cppWinResult = cppVarWin.ShowDialog();
					if (!cppWinResult.GetValueOrDefault())
						return;

					Properties.Settings.Default.LastDirectory = System.IO.Path.GetDirectoryName(fileName);
					Properties.Settings.Default.Save();
					View.CPPUsesVariable = true;
					View.CPPVariableFileName = fileName;
					View.CPPVariableData = cppVarWin.View.SelectedVariableIdentifier;

					using (var fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
					using (var sw = new StreamWriter(fs)) {
						sw.WriteLine("// " + cppVarWin.View.SelectedVariableIdentifier);
					}
					break;
				case "SelectAlternateFile":
					dir = string.IsNullOrEmpty(Properties.Settings.Default.LastDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : Properties.Settings.Default.LastDirectory;
					openDialog = new VistaOpenFileDialog
					{
						AddExtension = true,
						CheckFileExists = true,
						DefaultExt = View.ProjectType == VersionEngine.ProjectTypes.CSProject ? "cs" : View.ProjectType == VersionEngine.ProjectTypes.CPPProject ? "cpp" : "vb",
						Filter = "All Code files|*.cpp;*.cs;*.vb|C++ files|*.cpp|CSharp files|*.cs|VB files|*.vb|All files|*.*",
						InitialDirectory = dir,
						Multiselect = false,
						Title = "Select alternate version file..."
					};
					if (!openDialog.ShowDialog(this).GetValueOrDefault())
						return;
					fileName = openDialog.FileName;
					Properties.Settings.Default.LastDirectory = System.IO.Path.GetDirectoryName(fileName);
					Properties.Settings.Default.Save();
					View.IsAlternateFile = true;
					View.AssemblyInfoFileName = fileName;
					break;
			}
		}
	}
}
