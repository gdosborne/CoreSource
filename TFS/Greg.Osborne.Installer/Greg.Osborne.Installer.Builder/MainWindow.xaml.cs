namespace Greg.Osborne.Installer.Builder {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Windows;
	using System.Windows.Controls;
	using GregOsborne.Application;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Dialogs;

	public partial class MainWindow : Window {
		public MainWindow() {
			this.InitializeComponent();

			this.View.ExecuteUiAction += this.View_ExecuteUiAction;
			this.Loaded += this.Window_Loaded;
			this.Closing += this.Window_Closing;
			this.View.Initialize();

			Settings.ApplyWindowBounds(Application.Current.As<App>().ApplicationName, this, new Rect(100, 100, 500, 400));
		}

		private void View_ExecuteUiAction(object sender, GregOsborne.MVVMFramework.ExecuteUiActionEventArgs e) {
			if (e.CommandToExecute.Equals("GetNewDocument")) {

			} else if (e.CommandToExecute.Equals("SelectIcon")) {
				var win = new SelectIconWindow {
					Owner = this
				};
				//win.View.CurrentHex = (string)e.Parameters["hexvalue"];
				win.View.CurrentIndex = (int)e.Parameters["index"];
				var result = win.ShowDialog();
				if (!result.HasValue || !result.Value)
					return;
				e.Parameters["index"] = win.View.CurrentIndex;

			} else if (e.CommandToExecute.Equals("SaveAsInstallationController")) {
				var dlg = new VistaSaveFileDialog {
					AddExtension = true,
					CheckFileExists = false,
					CheckPathExists = true,
					DefaultExt = ".controller.xml",
					Filter = "Controller files|*.controller.xml",
					InitialDirectory = Path.GetDirectoryName(this.GetType().Assembly.Location),
					RestoreDirectory = true,
					Title = "Save installation controller as..."
				};
				var result = dlg.ShowDialog(this);
				if (!result.HasValue || !result.Value) {
					e.Parameters["canel"] = true;
				} else {
					e.Parameters["filename"] = dlg.FileName;
				}
			} else if (e.CommandToExecute.Equals("OpenInstallationController")) {
				var dlg = new VistaOpenFileDialog {
					AddExtension = true,
					CheckFileExists = true,
					CheckPathExists = true,
					DefaultExt = ".controller.xml",
					Filter = "Controller files|*.controller.xml",
					InitialDirectory = Path.GetDirectoryName(this.GetType().Assembly.Location),
					Multiselect = false,
					RestoreDirectory = true,
					Title = "Open installation controller..."
				};
				var result = dlg.ShowDialog(this);
				if (!result.HasValue || !result.Value) {
					e.Parameters["canel"] = true;
				} else {
					e.Parameters["filename"] = dlg.FileName;
				}
			} else if (e.CommandToExecute.Equals("ExitApplication")) {
				var cancelShutdown = this.ShutDown();
				if (cancelShutdown) {
					return;
				}

				Environment.Exit(0);
			}
		}

		public MainWindowView View => this.DataContext.As<MainWindowView>();

		private bool isCurrentlySizing = false;
		private Dictionary<ListView, double> originalWidths = new Dictionary<ListView, double>();
		private void UpdateColumnWidths(ListView sender) {
			var lvWidth = this.originalWidths.ContainsKey(sender) ? this.originalWidths[sender] : 0;
			if (this.isCurrentlySizing || lvWidth == sender.ActualWidth) {
				return;
			}

			var gv = sender.View.As<GridView>();
			var autoFillIndex = gv.Columns.Count - 1;
			this.isCurrentlySizing = true;
			var fullWidth = sender.ActualWidth;
			var otherWidths = 0.0;
			var columnCount = gv.Columns.Count;
			for (var i = 0; i < columnCount - 1; i++) {
				if (i < autoFillIndex) {
					otherWidths += gv.Columns[i].ActualWidth;
				}
			}
			if (fullWidth - otherWidths - 6 > 0) {
				gv.Columns[autoFillIndex].Width = fullWidth - otherWidths - 6;
			}

			if (!this.originalWidths.ContainsKey(sender)) {
				this.originalWidths.Add(sender, sender.ActualWidth);
			} else {
				this.originalWidths[sender] = sender.ActualWidth;
			}

			this.isCurrentlySizing = false;
		}

		private void ListView_SizeChanged(object sender, SizeChangedEventArgs e) => this.UpdateColumnWidths(sender.As<ListView>());

		private void ListView_Loaded(object sender, RoutedEventArgs e) => this.UpdateColumnWidths(sender.As<ListView>());

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Settings.SaveWindowBounds(Application.Current.As<App>().ApplicationName, this);
			Settings.SetSetting(Application.Current.As<App>().ApplicationName, this.GetType().Name, "VerticalSplitterPosition", this.SettingsBorder.ActualWidth);
			Settings.SetSetting(Application.Current.As<App>().ApplicationName, this.GetType().Name, "HorizontalSplitterPosition", this.SideItemsBorder.ActualHeight);
			e.Cancel = this.ShutDown();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			this.settingsColumn.Width = new GridLength(Settings.GetSetting(Application.Current.As<App>().ApplicationName, this.GetType().Name, "VerticalSplitterPosition", this.SettingsBorder.ActualWidth));
			this.sideItemsRow.Height = new GridLength(Settings.GetSetting(Application.Current.As<App>().ApplicationName, this.GetType().Name, "HorizontalSplitterPosition", this.SideItemsBorder.ActualHeight));
		}

		private bool ShutDown() {
			//this.View.Controller.HasChanges = true;
			if (this.View.Controller != null && this.View.Controller.HasChanges) {
				var td = new TaskDialog {
					AllowDialogCancellation = false,
					ButtonStyle = TaskDialogButtonStyle.Standard,
					CenterParent = true,
					Content = "The controller file contains changes that have not yet been saved. It is suggested you save the document. If you do not save, you will lose these changes.\n\nSave the document?",
					MainIcon = TaskDialogIcon.Warning,
					MainInstruction = "There are changes to the controller file",
					MinimizeBox = false,
					WindowTitle = "Exit application"
				};
				td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
				td.Buttons.Add(new TaskDialogButton(ButtonType.No));
				td.Buttons.Add(new TaskDialogButton(ButtonType.Cancel));
				var result = td.ShowDialog(this);
				if (result.ButtonType == ButtonType.Cancel) {
					return true;
				} else if (result.ButtonType == ButtonType.No) {
					//anything before we exit?
					return false;
				} else {
					this.View.Controller.Save();
				}
				return false;
			}
			return false;
		}

		private void GridTextBox_GotFocus(object sender, RoutedEventArgs e) => sender.As<TextBox>().SelectAll();
	}
}
