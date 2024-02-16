using GregOsborne.Application.Primitives;
using System.Windows;
using System.Windows.Media;
using VersionMaster;
using static GregOsborne.Application.Dialogs.DialogHelpers;

namespace ManageVersioning {
    public partial class ProjectWindow : Window {
        public ProjectWindow() {
            InitializeComponent();
            View.Initialize();
            View.PropertyChanged += (s, e) => {
                if (e.PropertyName == nameof(DialogResult)) {
                    if (View.DialogResult.HasValue && View.DialogResult.Value) {
                        if (View.Project != null) {
                            View.Project.HasChanges = View.itemHasChanges;
                        }
                    }
                    else {
                        //cancelled - revert to old values
                        if (View.Project != null) {
                            View.Project.FullPath = View.previousProjectPath;
                            View.Project.SelectedSchema = View.Schemas.FirstOrDefault(x => x.Name == View.previousSchemaName);
                            View.Project.HasChanges = false;
                        }
                    }
                    this.DialogResult = View.DialogResult;
                }
            };
            View.ExecuteUiAction += (s, e) => {
                if (Enum.TryParse(typeof(ProjectWindowView.UIActions), e.CommandToExecute, out var action)) {
                    switch (action) {
                        case ProjectWindowView.UIActions.EditSchema: {
                                var win = new SchemaWindow {
                                    Owner = this,
                                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                                };
                                win.View.DeleteVisibility = Visibility.Hidden;
                                win.View.Schema = View.Project.SelectedSchema;
                                win.View.Methods = View.Methods;
                                var foreground = App.GetResourceItem<SolidColorBrush>(App.Current.Resources, "WindowText");
                                var result = win.ShowDialog();
                                if (result.HasValue && result.Value) {
                                    win.View.Schema.HasChanges = true;
                                    View.UpdateInterface();
                                }
                            }
                            break;
                        case ProjectWindowView.UIActions.SelectProjectPath: {
                                View.previousProjectPath ??= View.Project != null
                                    ? View.Project.FullPath
                                    : default;
                                var lastPath = View.Project != null
                                    ? System.IO.Path.GetDirectoryName(View.Project.FullPath)
                                    : App.Session.ApplicationSettings.GetValue("Application", "LastProjectDirectory", default(string));
                                var filters = new List<(string extension, string name)> {
                                    ("*.csproj", "C# Projects")
                                };
                                var filename = ShowOpenFileDialog("Select project...", lastPath, string.Empty, [.. filters]);
                                if (!string.IsNullOrWhiteSpace(filename) && System.IO.File.Exists(filename)) {
                                    App.Session.ApplicationSettings.AddOrUpdateSetting("Application", "LastProjectDirectory", System.IO.Path.GetDirectoryName(filename));
                                    View.Project.FullPath = filename;
                                    View.itemHasChanges = true;
                                }
                            }
                            break;
                    }
                }
            };
            PathTextBox.GotFocus += (s, e) => {
                PathTextBox.SelectAll();
            };
            EditNameTextBox.IsVisibleChanged += (s, e) => {
                if ((bool)e.NewValue) {
                    EditNameTextBox.Focus();
                }
            };
            EditNameTextBox.GotFocus += (s, e) => {
                EditNameTextBox.SelectAll();
            };
            EditNameTextBox.PreviewKeyDown += (s, e) => {
                if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Tab) {
                    HideNameTextBox();
                    e.Handled = true;
                }
            };
            EditNameTextBox.LostFocus += (s, e) => {
                HideNameTextBox();
                e.Handled = true;
            };
        }

        private void HideNameTextBox() {
            View.EditControlVisibility = System.Windows.Visibility.Collapsed;
            View.TitleControlVisibility = System.Windows.Visibility.Visible;
            View.IsOKDefault = true;
        }

        public ProjectWindowView View => DataContext.As<ProjectWindowView>();

    }
}
