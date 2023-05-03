using System.Windows;
using System.Windows.Controls;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using GregOsborne.Dialogs;

namespace Greg.Osborne.Installer {
	public partial class MainWindow : Window {
		public MainWindow() {
			this.InitializeComponent();

			this.View.PropertyChanged += this.View_PropertyChanged;

			this.SizeChanged += this.MainWindow_SizeChanged;
			this.titleBorder.MouseDown += this.TitleBorder_MouseDown;

			this.View.ExecuteUiAction += this.View_ExecuteUiAction;

			this.InstructionBorder.SizeChanged += this.InstructionBorder_SizeChanged;

			this.View.Initialize();
		}

		private void InstructionBorder_SizeChanged(object sender, SizeChangedEventArgs e) => this.RightPadColumn.Width = new GridLength(sender.As<Border>().ActualWidth + 60);

		private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName.Equals("WindowSizeRatio")) {
				var ratio = this.View.WindowSizeRatio;
				if (ratio <= 0.3 || ratio >= 1.0) {
					ratio = .75;
				}

				this.Width = this.GetScreen().WorkingArea.Width * ratio;
				this.Height = this.GetScreen().WorkingArea.Height * ratio;
				this.CenterScreen();
			}
		}

		private void TitleBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => this.DragMove();

		private void View_ExecuteUiAction(object sender, GregOsborne.MVVMFramework.ExecuteUiActionEventArgs e) {
			var td = default(TaskDialog);

			if (e.CommandToExecute.Equals("CloseWindow")) {
				this.Close();
			} else if (e.CommandToExecute.Equals("MaximizeRestore")) {
				this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
			} else if (e.CommandToExecute.Equals("Minimize")) {
				this.WindowState = WindowState.Minimized;
			} else if (e.CommandToExecute.Equals("ProvideFeedback")) {

			} else if (e.CommandToExecute.Equals("CancelInstallation")) {
				td = new TaskDialog {
					MainInstruction = "Installation Canceled",
					AllowDialogCancellation = false,
					ButtonStyle = TaskDialogButtonStyle.Standard,
					CenterParent = true,
					Content = "You are indicating that you would like to cancel installation. If you stop now, no applications will be installed.\n\nAre you sure you want to cancel?",
					MainIcon = TaskDialogIcon.Warning,
					MinimizeBox = false,
					WindowTitle = "Installation canceled"
				};
				td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
				td.Buttons.Add(new TaskDialogButton(ButtonType.No));
				var result = td.ShowDialog(this);
				if(result.ButtonType == ButtonType.Yes) {
					App.Current.Shutdown();
				}
			} else if (e.CommandToExecute.Equals("ContinueInstallation")) {

			}
		}

		public MainWindowView View => this.DataContext.As<MainWindowView>();

		private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e) {
			var t = this.WindowState == WindowState.Maximized ? new Thickness(10, 10, 10, 0) : new Thickness(0);
			this.mainBorder.Margin = t;
		}

		private void TabbItem_TabSelected(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			this.instItemsTab.IsSelected = false;
			this.optionsTab.IsSelected = false;
			sender.As<TabbItem>().IsSelected = true;
			this.View.InstallationItemsVisibility = Visibility.Collapsed;
			this.View.OptionsVisibility = Visibility.Collapsed;
			if (sender.As<TabbItem>().HeaderText == this.instItemsTab.HeaderText) {
				this.View.InstallationItemsVisibility = Visibility.Visible;
			} else if (sender.As<TabbItem>().HeaderText == this.optionsTab.HeaderText) {
				this.View.OptionsVisibility = Visibility.Visible;
			} else {

			}
		}
	}
}
