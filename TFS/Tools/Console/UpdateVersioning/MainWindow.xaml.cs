using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using GregOsborne.Dialogs;
using System;
using System.Windows;
using sysio = System.IO;

namespace UpdateVersioning {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Closing += MainWindow_Closing;
            View.Initialize();
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void View_ExecuteUiAction(object sender, GregOsborne.MVVMFramework.ExecuteUiActionEventArgs e) {
            var act = (MainWindowView.Actions)Enum.Parse(typeof(MainWindowView.Actions), e.CommandToExecute);
            switch (act) {
                case MainWindowView.Actions.SaveProjects: {

                        break;
                    }
                case MainWindowView.Actions.NewProject: {
                        break;
                    }
                case MainWindowView.Actions.OpenProject: {
                        var lastProjectFileName = App.Session.ApplicationSettings.GetValue("General", "LastProjectFileName", string.Empty);
                        var lastProjectDirectory = App.Session.ApplicationSettings.GetValue("General", "LastProjectDirectory", string.Empty);
                        var filter = "C# Projects|*.csproj";
                        var dlg = new VistaOpenFileDialog {
                            CheckFileExists = true,
                            FileName = "*.csproj",
                            Filter = filter,
                            InitialDirectory = lastProjectDirectory,
                            Multiselect = false,
                            ShowReadOnly = false,
                            RestoreDirectory = true,
                            Title = "Select CSharp project file..."
                        };
                        var result1 = dlg.ShowDialog(this);
                        if (!result1.HasValue || !result1.Value) {
                            return;
                        }
                        App.Session.ApplicationSettings.AddOrUpdateSetting("General", "LastProjectFileName", dlg.FileName);
                        App.Session.ApplicationSettings.AddOrUpdateSetting("General", "LastProjectDirectory", sysio.Path.GetDirectoryName(dlg.FileName));
                        //View.ProjectFilename = dlg.FileName;
                        break;
                    }
            }
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            this.SavePosition("Update Versioning");
        }

        public MainWindowView View => DataContext.As<MainWindowView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.HideMinimizeAndMaximizeButtons();
            this.SetPosition("Update Versioning");
        }
    }
}
