using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using GregOsborne.Application.Windows.Controls;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.Serialization;
using VersionMaster;
using VersionMasterCore;
using static GregOsborne.Application.Dialogs.DialogHelpers;
using static GregOsborne.Application.IO.Directory;

namespace ManageVersioning {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            View.Initialize(this);
            View.ExecuteUiAction += View_ExecuteUiAction;
            TestItems = [];

            SetConsoleBrush();
            DragControl.MouseDown += (s, e) => {
                this.DragMove();
            };
            MenuGrid.MouseDown += (s, e) => {
                this.DragMove();
            };
            this.SizeChanged += (s, e) => {
                SetConsoleHeight(LeftDataGridRow.ActualHeight, LeftDataGridColumn.ActualWidth);
            };
        }

        private void SetConsoleBrush() {
            if (App.Settings.IsConsoleBackgroundBrushUsed && View.ConsoleImageBrush != null) {
                ConsoleTextBox.Background = View.ConsoleImageBrush;
                ConsoleTextBox.Foreground = new SolidColorBrush(App.Settings.ConsoleForeground);
            }
            else {
                ConsoleTextBox.Background = App.GetResourceItem<Brush>(App.Current.Resources, "ConsoleBackground");
                ConsoleTextBox.Foreground = App.GetResourceItem<Brush>(App.Current.Resources, "ConsoleForeground");
            }
        }

        private void View_ExecuteUiAction(object sender, GregOsborne.MVVMFramework.ExecuteUiActionEventArgs e) {
            if (Enum.TryParse(typeof(MainWindowView.UIActions), e.CommandToExecute, out var action)) {
                switch (action) {
                    case MainWindowView.UIActions.ShowAbout:
                        var tAttr = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>();
                        var dAttr = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyDescriptionAttribute>();
                        var vAttr = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>();
                        var cAttr = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>();
                        var crAttr = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>();
                        var msg = "";
                        var title = string.Empty;
                        if (tAttr != null) {
                            title=tAttr.Title;
                            msg += $"{tAttr.Title}\n"; ;
                        }
                        if (dAttr != null) {
                            msg += $"{dAttr.Description}\n";
                        }
                        if(vAttr != null) {
                            msg += $"Version {vAttr.Version}\n";
                        }
                        if(cAttr != null) {
                            msg += $"Company {cAttr.Company}\n";
                        }
                        if(crAttr != null) {
                            msg += $"{crAttr.Copyright}\n";
                        }
                        Dialogs.ShowOKDialog($"About {title}", msg, Ookii.Dialogs.Wpf.TaskDialogIcon.Information, 200);
                        break;
                    case MainWindowView.UIActions.Minimize:
                        WindowState = WindowState.Minimized;
                        break;
                    case MainWindowView.UIActions.Maximize:
                        WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
                        TitlebarGrid.Margin = WindowState == WindowState.Maximized ? new Thickness(4, 4, 4, 0) : new Thickness(0, 0, 0, 0);
                        BodyGrid.Margin = WindowState == WindowState.Maximized ? new Thickness(4, 0, 4, 0) : new Thickness(0, 0, 0, 0);
                        break;
                    case MainWindowView.UIActions.Delete: {

                            var result = Dialogs.ShowYesNoDialog("Delete Project", $"You are about to delete the version data for the project " +
                                $"\"{View.SelectedProject.Name}\". If you delete this version data the project version will no longer be updated." +
                                $"\n\nDelete the project?", Ookii.Dialogs.Wpf.TaskDialogIcon.Warning);
                            if (!result)
                                return;

                            View.HasDeletedItems = true;
                            View.Projects.Remove(View.SelectedProject);
                            View.UpdateInterface();
                        }
                        break;
                    case MainWindowView.UIActions.CutValue: {
                            var cloned = View.SelectedProject.Clone().As<ProjectData>();
                            cloned.Name = $"{cloned.Name} [cut]";
                            View.UndoItems.Push(new(cloned, MainWindowView.OriginalActions.Cut, null, null));
                            View.SelectedProject.Foreground = App.GetResourceItem<SolidColorBrush>(App.Current.Resources, "DeletedForeground");
                            View.SelectedProject.HasChanges = true;
                            View.SelectedProject.IsDeleted = true;
                            View.SelectedProject = null;
                        }
                        break;

                    case MainWindowView.UIActions.PasteValue: {
                            var retrievedData = default(ProjectData);
                            var showCannotPaste = false;
                            if (View.UndoItems.Count == 0)
                                showCannotPaste = true;
                            else if (View.UndoItems.TryPeek(out var value)) {
                                if (value.Action == MainWindowView.OriginalActions.Cut || value.Action == MainWindowView.OriginalActions.Copy) {
                                    retrievedData = View.UndoItems.Pop().Item;
                                }
                                else
                                    showCannotPaste = true;
                            }
                            if (showCannotPaste) {
                                ShowOKDialog("Cannot paste", "The last item in the local clipboard cannot be pasted into the project list. The last action must " +
                                    "be either a Cut or a Copy Action.", Ookii.Dialogs.Wpf.TaskDialogIcon.Warning);
                                return;
                            }
                            if (retrievedData == null)
                                return;

                            retrievedData.Foreground = App.GetResourceItem<SolidColorBrush>(App.Current.Resources, "WindowText");
                            retrievedData.IsDeleted = false;
                            retrievedData.IsInserted = false;
                            var win = new ProjectWindow {
                                Owner = this,
                                WindowStartupLocation = WindowStartupLocation.CenterOwner
                            };
                            win.View.Project = retrievedData;
                            win.View.Project.SelectedSchema = retrievedData.SelectedSchema;
                            var result = win.ShowDialog();
                            if (result.HasValue && result.Value) {
                                retrievedData.HasChanges = true;
                                View.Projects.Add(retrievedData);
                            }
                        }
                        break;

                    case MainWindowView.UIActions.CopyValue: {
                            var cloned = View.SelectedProject.Clone().As<ProjectData>();
                            cloned.Name = $"{cloned.Name} [copied]";
                            View.UndoItems.Push(new(cloned, MainWindowView.OriginalActions.Copy, null, null));
                        }
                        break;

                    case MainWindowView.UIActions.UndoChange: {
                            if (View.UndoItems.TryPeek(out var value)) {
                                var item = value.Item;
                                var realItem = View.UndoItems.Pop();
                                switch (value.Action) {
                                    case MainWindowView.OriginalActions.Delete:
                                    case MainWindowView.OriginalActions.Cut: {
                                            realItem.Item.Foreground = App.GetResourceItem<SolidColorBrush>(App.Current.Resources, "WindowText");
                                            realItem.Item.IsDeleted = false;
                                            realItem.Item.IsInserted = false;
                                            realItem.Item.HasChanges = false;
                                            View.RedoItems.Push(realItem);
                                        }
                                        break;
                                    case MainWindowView.OriginalActions.Insert: {
                                            realItem.Item.Foreground = App.GetResourceItem<SolidColorBrush>(App.Current.Resources, "InsertedForeground");
                                            realItem.Item.IsDeleted = false;
                                            realItem.Item.HasChanges = false;
                                            realItem.Item.IsInserted = true;
                                        }
                                        break;
                                    case MainWindowView.OriginalActions.Copy: {
                                            //nothing to do
                                        }
                                        break;
                                }
                            }
                        }
                        break;

                    case MainWindowView.UIActions.RedoChange: {
                            if (View.RedoItems.TryPeek(out var value)) {
                                var item = value.Item;
                                var realItem = View.RedoItems.Pop();

                                switch (value.Action) {
                                    case MainWindowView.OriginalActions.Delete:
                                    case MainWindowView.OriginalActions.Cut: {
                                            realItem.Item.Foreground = App.GetResourceItem<SolidColorBrush>(App.Current.Resources, "InsertedForeground");
                                            realItem.Item.IsDeleted = false;
                                            realItem.Item.HasChanges = false;
                                            realItem.Item.IsInserted = true;

                                            View.RedoItems.Push(realItem);
                                        }
                                        break;
                                    case MainWindowView.OriginalActions.Insert: {
                                            realItem.Item.Foreground = App.GetResourceItem<SolidColorBrush>(App.Current.Resources, "WindowText");
                                            realItem.Item.IsDeleted = false;
                                            realItem.Item.IsInserted = false;
                                            realItem.Item.HasChanges = false;
                                        }
                                        break;
                                    case MainWindowView.OriginalActions.Copy: {
                                            //nothing to do
                                        }
                                        break;
                                }
                            }
                        }
                        break;

                    case MainWindowView.UIActions.AddNewProject: {
                            var win = new ProjectWindow {
                                Owner = this,
                                WindowStartupLocation = WindowStartupLocation.CenterOwner
                            };
                            var foreground = App.GetResourceItem<SolidColorBrush>(App.Current.Resources, "WindowText");
                            win.View.Project = new VersionMaster.ProjectData(foreground) {
                                IsNewItem = true,
                                AllTransformTypes = View.Projects.First().AllTransformTypes,
                                Name = "Not set"
                            };
                            win.View.Schemas = View.Schemas;
                            win.View.Project.SelectedSchema = View.Schemas.FirstOrDefault(x => x.Name == "default");
                            var result = win.ShowDialog();
                            if (result.HasValue && result.Value) {
                                if (View.Projects.Any(x => x.Name.EqualsIgnoreCase(win.View.Project.Name))) {
                                    Dialogs.ShowOKDialog("Error", $"The project name \"{win.View.Project.Name}\" is not unique.", Ookii.Dialogs.Wpf.TaskDialogIcon.Warning);
                                    return;
                                }
                                else {
                                    win.View.Project.HasChanges = true;
                                    View.Projects.Add(win.View.Project);
                                    View.UpdateInterface();
                                }
                            }
                        }
                        break;

                    case MainWindowView.UIActions.AddNewSchema: {
                            var win = new SchemaWindow {
                                Owner = this,
                                WindowStartupLocation = WindowStartupLocation.CenterOwner
                            };
                            win.View.DeleteVisibility = Visibility.Hidden;
                            win.View.Methods = View.Methods;
                            var foreground = App.GetResourceItem<SolidColorBrush>(App.Current.Resources, "WindowText");
                            win.View.Schema = new VersionMaster.SchemaItem(foreground) {
                                IsNewItem = true,
                                Name = "Not set",
                                MajorPart = Enumerations.TransformTypes.Ignore,
                                MinorPart = Enumerations.TransformTypes.Ignore,
                                BuildPart = Enumerations.TransformTypes.Ignore,
                                RevisionPart = Enumerations.TransformTypes.Ignore
                            };
                            var result = win.ShowDialog();
                            if (result.HasValue && result.Value) {
                                if (View.Schemas.Any(x => x.Name.EqualsIgnoreCase(win.View.Schema.Name))) {
                                    Dialogs.ShowOKDialog("Error", $"The schema name \"{win.View.Schema.Name}\" is not unique.", Ookii.Dialogs.Wpf.TaskDialogIcon.Warning);
                                    return;
                                }
                                else {
                                    win.View.Schema.HasChanges = true;
                                    View.Schemas.Add(win.View.Schema);
                                    View.UpdateInterface();
                                }
                            }
                        }
                        break;

                    case MainWindowView.UIActions.ExitApplication:
                        Close();
                        break;

                    case MainWindowView.UIActions.ShowSettings: {
                            var win = new SettingsWindow {
                                Owner = this,
                                WindowStartupLocation = WindowStartupLocation.Manual,
                            };
                            win.View.AreWindowPositionsSaved = App.Settings.AreWindowPositionsSaved;
                            win.View.IsConsoleEditable = App.Settings.IsTestConsoleEditable;
                            win.View.IsConsoleBackgroundBrushUsed = App.Settings.IsConsoleBackgroundBrushUsed;
                            win.View.ConsoleBrushFilePath = App.Settings.ConsoleBrushFilePath;
                            win.View.ConsoleImageForegroundColor = new SolidColorBrush(App.Settings.ConsoleForeground);
                            win.View.ConsoleImageOpacity = App.Settings.ConsoleBrushOpacity;

                            var result = win.ShowDialog();
                            if (!result.HasValue || !result.Value)
                                return;

                            App.Settings.AreWindowPositionsSaved = win.View.AreWindowPositionsSaved;
                            App.Settings.IsTestConsoleEditable = win.View.IsConsoleEditable;
                            App.Settings.IsConsoleBackgroundBrushUsed = win.View.IsConsoleBackgroundBrushUsed;
                            App.Settings.ConsoleBrushFilePath = win.View.ConsoleBrushFilePath;
                            App.Settings.ConsoleBrushOpacity = win.View.ConsoleImageOpacity;
                            View.ConsoleImageBrush = GregOsborne.Application.Media.Extensions.GetImageBrush(App.Settings.ConsoleBrushFilePath, ((double)App.Settings.ConsoleBrushOpacity) / 100.0);
                            App.Settings.ConsoleForeground = win.View.ConsoleImageForegroundColor.As<SolidColorBrush>().Color;

                            ConsoleTextBox.IsReadOnly = !App.Settings.IsTestConsoleEditable;

                            SetConsoleBrush();

                        }
                        break;

                    case MainWindowView.UIActions.TestVersion: {
                            var item = TestItems.FirstOrDefault(x => x.Item.FullPath.EqualsIgnoreCase(View.SelectedProject.FullPath));
                            if (item == null) {
                                item = new TestData(View.SelectedProject);
                                TestItems.Add(item);
                            }
                            var savedVersion = View.SelectedProject.CurrentAssemblyVersion;
                            var savedLastBuidDate = View.SelectedProject.LastBuildDate;
                            var lastRun = item.LastRun;
                            if (lastRun != null) {
                                View.SelectedProject.CurrentAssemblyVersion = lastRun.EndVersion;
                                View.SelectedProject.LastBuildDate = lastRun.RunDate;
                            }
                            var newRun = new TestRun {
                                RunDate = DateTime.Now,
                                StartVersion = View.SelectedProject.CurrentAssemblyVersion
                            };
                            item.Runs.Add(newRun);

                            View.SelectedProject.ReportProgress += SelectedProject_ReportProgress;
                            View.SelectedProject.ModifyVersion();
                            View.ConsoleText += Environment.NewLine;
                            View.SelectedProject.ReportProgress -= SelectedProject_ReportProgress;

                            newRun.EndVersion = View.SelectedProject.ModifiedAssemblyVersion;
                            View.SelectedProject.CurrentAssemblyVersion = savedVersion;
                            View.SelectedProject.LastBuildDate = savedLastBuidDate;
                        }
                        break;

                    case MainWindowView.UIActions.SaveData:
                        var doc = new XDocument();
                        var root = new XElement("updateversion.projects", new XAttribute("version", "2.0"));
                        var projectsRoot = new XElement("projects");
                        var schemaRoot = new XElement("schemas");
                        var methodsRoot = new XElement("methods");
                        View.Schemas.ToList().ForEach(x => {
                            x.HasChanges = false;
                            schemaRoot.Add(x.ToXElement());
                        });
                        View.Methods.ToList().ForEach(x => {
                            var name = x.ToString();
                            var val = (int)x;
                            methodsRoot.Add(new XElement("method", new XAttribute("name", name), new XAttribute("value", val)));
                        });
                        View.Projects.ToList().ForEach(x => {
                            x.HasChanges = false;
                            projectsRoot.Add(x.ToXElement());
                        });
                        root.Add(schemaRoot);
                        root.Add(methodsRoot);
                        root.Add(projectsRoot);
                        doc.Add(root);
                        doc.Save(View.DataFilePath);
                        View.HasDeletedItems = false;
                        View.UpdateInterface();
                        break;

                    case MainWindowView.UIActions.GotoDataFileDirectory:
                        new SysIO.DirectoryInfo(SysIO.Path.GetDirectoryName(View.DataFilePath)).ShowInExplorer();
                        break;

                    case MainWindowView.UIActions.EditProject: {
                            var win = new ProjectWindow {
                                Owner = this,
                                WindowStartupLocation = WindowStartupLocation.CenterOwner
                            };
                            win.View.Schemas = View.Schemas;
                            var prev = View.SelectedProject.Clone().As<ProjectData>();
                            win.View.Project = View.SelectedProject;
                            var result = win.ShowDialog();
                            if (result.HasValue && result.Value) {
                                View.SelectedProject.HasChanges = true;
                            }
                            else {
                                View.SelectedProject.Revert(prev);
                            }
                            View.UpdateInterface();
                        }
                        break;
                }
            }
        }

        private List<TestData> TestItems { get; set; }

        private void SelectedProject_ReportProgress(object sender, ReportProgressEventArgs e) {
            View.ConsoleText += $"{e.Message}\n";
            ConsoleTextBox.ScrollToEnd();
        }

        private bool AskSaveData() =>
            ShowYesNoDialog("Data has changed", "The schema or project data has changed. If you do not " +
                "save now, your changes will be lost.\n\nSave changes?", Ookii.Dialogs.Wpf.TaskDialogIcon.Shield);

        public MainWindowView View => DataContext.As<MainWindowView>();

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            if (View.HasChanges) {
                if (AskSaveData()) {
                    View.SaveCommand.Execute(null);
                }
            }
            this.SavePosition(App.Session.ApplicationSettings);

            App.Session.ApplicationSettings.AddOrUpdateSetting(this.GetType().Name, "PrimaryDataGridWidth", ProjectsDataGrid.ActualWidth);
            App.Session.ApplicationSettings.AddOrUpdateSetting(this.GetType().Name, "PrimaryDataGridHeight",
                ProjectsDataGrid.ActualHeight + MainToolbar.ActualHeight);
        }

        private void SetConsoleHeight(double height, double width) {
            var desiredHeight = (BodyGrid.ActualHeight - 4 - Status.ActualHeight - MainToolbar.ActualHeight) * .65;
            var desiredWidth = (BodyGrid.ActualWidth - 4) * .75;
            width = width < 200 ? desiredWidth : width;
            height = height < 200 ? desiredHeight : height;
            height = height > desiredHeight ? desiredHeight : height;
            width = width > desiredWidth ? desiredWidth : width;
            LeftDataGridRow.Height = new GridLength(height);
            LeftDataGridColumn.Width = new GridLength(width);
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            if (App.Settings.AreWindowPositionsSaved) {
                this.SetPosition(App.Session.ApplicationSettings);
                TitlebarGrid.Margin = WindowState == WindowState.Maximized ? new Thickness(4, 4, 4, 0) : new Thickness(0, 0, 0, 0);
                BodyGrid.Margin = WindowState == WindowState.Maximized ? new Thickness(4, 0, 4, 0) : new Thickness(0, 0, 0, 0);

                var topHeight = App.Session.ApplicationSettings.GetValue(this.GetType().Name, "PrimaryDataGridHeight", this.ActualHeight / 2);
                var topWidth = App.Session.ApplicationSettings.GetValue(this.GetType().Name, "PrimaryDataGridWidth", this.ActualWidth / 2);
                SetConsoleHeight(topHeight, topWidth);
            }
            ConsoleTextBox.IsReadOnly = !App.Settings.IsTestConsoleEditable;
        }

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (View.SelectedProject == null) return;
            View.EditProjectCommand.Execute(null);
        }

        private void DataGrid_MouseDoubleClick1(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (View.SelectedSchema == null) return;
            var win = new SchemaWindow {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            win.View.DeleteVisibility = View.SelectedSchema.Name.EqualsIgnoreCase("default") ? Visibility.Hidden : Visibility.Visible;
            win.View.Schema = View.SelectedSchema;
            win.View.Methods = View.Methods;
            var result = win.ShowDialog();
            if (result.HasValue && result.Value) {
                if (win.View.IsDelete) {
                    var result1 = Dialogs.ShowYesNoDialog("Delete Schema", $"You are about to delete the schema " +
                        $"\"{View.SelectedSchema.Name}\". If you delete this schema any version items that use this " +
                        $"schema will be changed to \"default\".\n\nDelete the schema?", Ookii.Dialogs.Wpf.TaskDialogIcon.Warning);
                    if (!result1)
                        return;
                    View.Projects.Where(x => x.SelectedSchema.Name.Equals(View.SelectedSchema)).ToList().ForEach(x => {
                        x.SelectedSchema = View.Schemas.FirstOrDefault(y => y.Name.EqualsIgnoreCase("default"));
                    });
                    View.Schemas.Remove(View.SelectedSchema);
                    View.SelectedSchema = null;
                }
                View.HasDeletedItems = true;
                View.UpdateInterface();
            }

        }

    }
}