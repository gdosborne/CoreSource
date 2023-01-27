using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationManager.ViewModels;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Path = System.IO.Path;

namespace CongregationManager {
    public partial class ExtensionManagerWindow : Window {
        public ExtensionManagerWindow() {
            InitializeComponent();

            Closing += ExtensionManagerWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
            View.Initialize();
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            this.SetBounds(App.ApplicationSession.ApplicationSettings);
        }

        private void ExtensionManagerWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            this.SaveBounds(App.ApplicationSession.ApplicationSettings);
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (ExtensionManagerWindowViewModel.Actions)Enum.Parse(typeof(ExtensionManagerWindowViewModel.Actions), e.CommandToExecute);
            switch (action) {
                case ExtensionManagerWindowViewModel.Actions.CloseWindow: {
                        Close();
                        break;
                    }
                case ExtensionManagerWindowViewModel.Actions.AddNewExtension: {
                        var lastDir = App.ApplicationSession.ApplicationSettings.GetValue("Application", "LastExtensionDir",
                            Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

                        var ofd = new VistaOpenFileDialog {
                            Title = "Select extension file",
                            CheckFileExists = true,
                            CheckPathExists = true,
                            DefaultExt = ".cmextension",
                            Filter = "Congregation Manager Extension (*.dll)|*.dll",
                            InitialDirectory = lastDir,
                            Multiselect = true,
                            RestoreDirectory = true
                        };
                        var result = ofd.ShowDialog(this);
                        if (!result.HasValue || !result.Value)
                            return;
                        App.AddExtensions(ofd.FileNames);
                        ofd.FileNames.ToList().ForEach(x => {
                            var fname = Path.GetFileName(x);
                            File.Copy(x, Path.Combine(App.ExtensionsFolder, fname), true);
                        });
                        var dt = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
                        dt.Tick += Dt_Tick;
                        App.ApplicationSession.ApplicationSettings.AddOrUpdateSetting("Application", "LastExtensionDir",
                            Path.GetDirectoryName(ofd.FileNames[0]));
                        dt.Start();
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
                            var name = View.SelectedExtension.Name;
                            View.SelectedExtension.Destroy();
                            ApplicationData.Extensions.Remove(View.SelectedExtension);
                            View.Extensions.Remove(View.SelectedExtension);
                            View.SelectedExtension = null;
                            GC.Collect();
                            if (File.Exists(filename)) {
                                File.Delete(filename);
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void Dt_Tick(object? sender, EventArgs e) {
            sender.As<DispatcherTimer>().Stop();
            var rdSource = "/CongregationManager;component/Resources/MainTheme.xaml";
            var myResourceDictionary = new ResourceDictionary {
                Source = new Uri(rdSource, UriKind.RelativeOrAbsolute)
            };

            View.Extensions.Clear();
            ApplicationData.Extensions.ForEach(x => {
                x.SaveExtensionData += this.Owner.As<MainWindow>().SaveExtensionData;
                x.AddControlItem += this.Owner.As<MainWindow>().AddControlItem;
                x.RemoveControlItem += this.Owner.As<MainWindow>().RemoveControlItem;
                x.Initialize(App.DataFolder, App.TempFolder,
                    App.ApplicationSession.ApplicationSettings,
                    App.ApplicationSession.Logger, App.DataManager);
                View.Extensions.Add(x);
            });

        }

        public ExtensionManagerWindowViewModel View => DataContext.As<ExtensionManagerWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
    }
}
