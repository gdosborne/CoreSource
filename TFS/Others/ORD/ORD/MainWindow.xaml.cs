// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
//
//
namespace SNC.Applications.Developer
{
	using MVVMFramework;
	using Ookii.Dialogs.Wpf;
	using ORDControls.ItemProperties;
	using ORDModel;
	using SNC.Applications.Developer.Views;
	using System;
	using System.IO;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;

	/// <summary>
	/// Class MainWindow.
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Private Methods
		private void MainWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e) {
			TaskDialogButton yesButton = new TaskDialogButton(ButtonType.Yes);
			TaskDialogButton noButton = new TaskDialogButton(ButtonType.No);
			TaskDialogButton cancelButton = new TaskDialogButton(ButtonType.Cancel);
			TaskDialogButton okButton = new TaskDialogButton(ButtonType.Ok);
			switch (e.CommandToExecute) {
				case "ShowErrorMessage":
					var errorTaskDialog = new TaskDialog
					{
						AllowDialogCancellation = true,
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						MainIcon = TaskDialogIcon.Error,
						MainInstruction = (string)e.Parameters["message"],
						MinimizeBox = false,
						WindowTitle = "Error"
					};
					if (e.Parameters.ContainsKey("footer"))
						errorTaskDialog.Footer = (string)e.Parameters["footer"];
					if (e.Parameters.ContainsKey("additionalinfo"))
						errorTaskDialog.ExpandedInformation = (string)e.Parameters["additionalinfo"];
					errorTaskDialog.Buttons.Add(okButton);
					errorTaskDialog.ShowDialog(this);
					break;
				case "DisplayProjectChangedMessage":
					var projectChangedTaskDialog = new TaskDialog
					{
						AllowDialogCancellation = true,
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						ExpandedInformation = View.ProjectFileName,
						Footer = "If you answer no to this dialog, your changes will be lost.",
						MainIcon = TaskDialogIcon.Information,
						MainInstruction = "The currently loaded project has changes. Would you like to save the loaded project?",
						MinimizeBox = false,
						WindowTitle = "Project changed"
					};
					projectChangedTaskDialog.Buttons.Add(yesButton);
					projectChangedTaskDialog.Buttons.Add(noButton);
					projectChangedTaskDialog.Buttons.Add(cancelButton);
					var projectChangedResult = projectChangedTaskDialog.ShowDialog(this);
					if (projectChangedResult == yesButton) {
						e.Parameters.Add("result", View.SaveProject());
						return;
					}
					else if (projectChangedResult == cancelButton) {
						e.Parameters.Add("result", null);
						return;
					}
					else {
						e.Parameters.Add("result", true);
						//do nothing
					}
					View.IsProjectChanged = false;
					if (e.Parameters["action"].Equals("new")) {
						View.NewProjectCommand.Execute(null);
					}
					else if (e.Parameters["action"].Equals("open")) {
						View.OpenProjectCommand.Execute(null);
					}
					break;
				case "CreateNewProject":
					var saveFileDialog = new VistaSaveFileDialog
					{
						AddExtension = true,
						CheckFileExists = false,
						CheckPathExists = true,
						CreatePrompt = false,
						DefaultExt = "xml",
						Filter = "Project files|*.xml",
						InitialDirectory = string.IsNullOrEmpty(Properties.Settings.Default.LastOpenDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : Properties.Settings.Default.LastOpenDirectory,
						OverwritePrompt = true,
						Title = "Create new project..."
					};
					var saveFileResult = saveFileDialog.ShowDialog(this);
					if (!saveFileResult.GetValueOrDefault())
						return;
					Properties.Settings.Default.LastOpenDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
					Properties.Settings.Default.Save();
					View.CreateProject(saveFileDialog.FileName);
					break;
				case "OpenProject":
					var openFileDialog = new VistaOpenFileDialog
					{
						AddExtension = true,
						CheckFileExists = true,
						CheckPathExists = true,
						DefaultExt = "xml",
						Filter = "Project files|*.xml",
						InitialDirectory = string.IsNullOrEmpty(Properties.Settings.Default.LastOpenDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : Properties.Settings.Default.LastOpenDirectory,
						Title = "Open project..."
					};
					var openFileResult = openFileDialog.ShowDialog(this);
					if (!openFileResult.GetValueOrDefault())
						return;
					Properties.Settings.Default.LastOpenDirectory = Path.GetDirectoryName(openFileDialog.FileName);
					Properties.Settings.Default.Save();
					View.OpenProject(openFileDialog.FileName);
					break;
				case "ShowObjectProperties":
					var propWin = new PropertiesWindow
					{
						WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
						Owner = this
					};
					var item = (Project)e.Parameters["object"];
					var propCtrl = ((IPropertyItem)e.Parameters["object"]).GetPropertiesControl();
					UserControl ctrl = null;
					if (item is Project) {
						ctrl = (ProjectProperties)propCtrl;
						ctrl.SetValue(Grid.RowProperty, 0);
						ctrl.SetValue(Grid.ColumnProperty, 0);
						ctrl.SetValue(ProjectProperties.ProjectNameProperty, item.Name);
						ctrl.SetValue(ProjectProperties.ProjectDescriptionProperty, item.Description);
						ctrl.SetValue(ProjectProperties.ProjectLocationProperty, item.FileName);
						ctrl.SetValue(ProjectProperties.ProjectSizeProperty, SNC.OptiRamp.Application.Extensions.IO.File.Size(item.FileName));
						ctrl.SetValue(ProjectProperties.RevisionsItemsSourceProperty, item.Revisions.OrderByDescending(x => x.Created));
					}
					propWin.ContentGrid.Children.Add(ctrl);
					var result1 = propWin.ShowDialog();
					if (!result1.GetValueOrDefault())
						return;
					if (item is Project) {
						item.Name = ((ProjectProperties)ctrl).View.ProjectName;
						item.Description = ((ProjectProperties)ctrl).View.ProjectDescription;
					}
					break;
			}
		}
		private void MainWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
		}
		private void TreeView_GotFocus(object sender, RoutedEventArgs e) {
		}
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Properties.Settings.Default.WindowState = this.WindowState;
			Properties.Settings.Default.MainWindowLocation = new System.Drawing.Point(Convert.ToInt32(this.RestoreBounds.Left), Convert.ToInt32(this.RestoreBounds.Top));
			Properties.Settings.Default.MainWindowSize = new System.Drawing.Size(Convert.ToInt32(this.RestoreBounds.Width), Convert.ToInt32(this.RestoreBounds.Height));
			Properties.Settings.Default.Save();
			if (View.IsProjectChanged) {
				var result = (bool?)View.DisplayProjectChangedNotification("close");
				if (!result.HasValue)
					e.Cancel = true;
			}
		}
		private void Window_Loaded(object sender, RoutedEventArgs e) {
			this.Left = Properties.Settings.Default.MainWindowLocation.X;
			this.Top = Properties.Settings.Default.MainWindowLocation.Y;
			this.Width = Properties.Settings.Default.MainWindowSize.Width;
			this.Height = Properties.Settings.Default.MainWindowSize.Height;
			this.WindowState = Properties.Settings.Default.WindowState;
		}
		#endregion Private Methods

		#region Public Properties
		/// <summary>
		/// Gets the view.
		/// </summary>
		/// <value>The view.</value>
		public MainWindowView View {
			get {
				return LayoutRoot.GetView<MainWindowView>();
			}
		}
		#endregion Public Properties
	}
}