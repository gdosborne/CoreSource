using Common.Applicationn.Linq;
using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationManager.ViewModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Common.Applicationn.Logging.ApplicationLogger;
using Path = System.IO.Path;

namespace CongregationManager {
    public partial class ExtensionManagerWindow : Window {
        public ExtensionManagerWindow() {
            InitializeComponent();

            App.LogMessage("Opening extension manager", EntryTypes.Information);
            Closing += ExtensionManagerWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
            View.Initialize();

            //View.Extensions.CollectionChanged += Extensions_CollectionChanged;
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            this.SetBounds(App.ApplicationSession.ApplicationSettings);
        }

        private void ExtensionManagerWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            this.SaveBounds(App.ApplicationSession.ApplicationSettings);
            App.LogMessage("Closing extension manager", EntryTypes.Information);
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (ExtensionManagerWindowViewModel.Actions)Enum.Parse(typeof(ExtensionManagerWindowViewModel.Actions), e.CommandToExecute);
            switch (action) {
                case ExtensionManagerWindowViewModel.Actions.CloseWindow: {
                        Close();
                        break;
                    }
                case ExtensionManagerWindowViewModel.Actions.AddNewExtension: {
                        var lastDir = App.ApplicationSession.ApplicationSettings.GetValue(
                            "Application", "LastExtensionDir",
                            Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                        if (!Directory.Exists(lastDir))
                            lastDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        var filenames = App.SelectFileDialog(this, "Select extension file(s)", ".dll",
                            App.FileFilters, lastDir, true);
                        if (!filenames.Any())
                            return;

                        App.ApplicationSession.ApplicationSettings.AddOrUpdateSetting("Application", "LastExtensionDir",
                           Path.GetDirectoryName(filenames[0]));

                        filenames.ToList().ForEach(x => {
                            var fname = Path.GetFileName(x);
                            App.LogMessage($"  Adding extension {fname}", EntryTypes.Information);
                            File.Copy(x, Path.Combine(App.ExtensionsFolder, fname), true);
                        });
                        //var extCount = ApplicationData.Extensions.Count;
                        //var sw = new Stopwatch();
                        //sw.Start();
                        //while(sw.ElapsedMilliseconds < 10000 || ApplicationData.Extensions.Count > extCount) {
                        //    Task.Delay(100);
                        //}
                        //View.Extensions.Clear();
                        //View.Extensions.AddRange(ApplicationData.Extensions);
                        break;
                    }
                case ExtensionManagerWindowViewModel.Actions.DeleteExtension: {
                        var result = App.IsYesInDialogSelected("Delete Currently Selected Extension",
                            $"You have selected to delete the \"{View.SelectedExtension.Name}\" Extension. " +
                            $"If you continue, the extension will not be available until you add it again.\n\n" +
                            $"Are you sure you want to continue?",
                            "Delete Extension", Ookii.Dialogs.Wpf.TaskDialogIcon.Warning);
                        if (result) {
                            var filename = View.SelectedExtension.Filename;
                            App.LogMessage($"Removing extension {filename}", EntryTypes.Information);
                            if (File.Exists(View.SelectedExtension.Filename)) {
                                File.Delete(View.SelectedExtension.Filename);
                                View.Extensions.Remove(View.SelectedExtension);
                            }
                            View.SelectedExtension = null;
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public ExtensionManagerWindowViewModel View => DataContext.As<ExtensionManagerWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
    }
}
