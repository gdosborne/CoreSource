using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationManager.Extensibility;
using CongregationManager.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using Path = System.IO.Path;

namespace CongregationManager {
    public partial class ExtensionManagerWindow : Window {
        public ExtensionManagerWindow() {
            InitializeComponent();

            Closing += ExtensionManagerWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
            View.Initialize();

            //View.Extensions.CollectionChanged += Extensions_CollectionChanged;
        }

        //private void Extensions_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
        //    switch (e.Action) {
        //        case System.Collections.Specialized.NotifyCollectionChangedAction.Add: {
        //                //if (e.NewItems.Count > 0) {
        //                //    foreach (var item in e.NewItems) {
        //                //        var ext = item.As<ExtensionBase>();

        //                //        ext.SaveExtensionData += this.Owner.As<MainWindow>().SaveExtensionData;
        //                //        ext.AddControlItem += this.Owner.As<MainWindow>().AddControlItem;
        //                //        ext.RemoveControlItem += this.Owner.As<MainWindow>().RemoveControlItem;
        //                //        ext.Initialize(App.DataFolder, App.TempFolder,
        //                //            App.ApplicationSession.ApplicationSettings,
        //                //            App.ApplicationSession.Logger, App.DataManager);

        //                //        var win = Owner.As<MainWindow>();
        //                //        win.View.Panels.Add(ext.Panel);

        //                //        break;
        //                //    }
        //                //}
        //                break;
        //            }
        //        case System.Collections.Specialized.NotifyCollectionChangedAction.Remove: {
        //                //if (e.OldItems.Count > 0) {
        //                //    foreach (var item in e.OldItems) {
        //                //        var ext = item.As<ExtensionBase>();
        //                //        ext.SaveExtensionData -= this.Owner.As<MainWindow>().SaveExtensionData;
        //                //        ext.AddControlItem -= this.Owner.As<MainWindow>().AddControlItem;
        //                //        ext.RemoveControlItem -= this.Owner.As<MainWindow>().RemoveControlItem;
        //                //        if(ext.Panel.Control.Parent != null)
        //                //            ext.Panel.Control.Parent.RemoveChild(ext.Panel.Control);
        //                //    }
        //                //}
        //                break;
        //            }
        //        case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
        //        case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
        //        case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
        //        default:
        //            break;
        //    }
        //}

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
                        var lastDir = App.ApplicationSession.ApplicationSettings.GetValue(
                            "Application", "LastExtensionDir",
                            Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                        if (!Directory.Exists(lastDir))
                            lastDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        var filenames = App.SelectFileDialog(this, "Select extension file(s)", ".cmextension",
                            App.FileFilters, lastDir, true);
                        if (!filenames.Any())
                            return;

                        App.ApplicationSession.ApplicationSettings.AddOrUpdateSetting("Application", "LastExtensionDir",
                           Path.GetDirectoryName(filenames[0]));

                        filenames.ToList().ForEach(x => {
                            var fname = Path.GetFileName(x);
                            File.Copy(x, Path.Combine(App.ExtensionsFolder, fname), true);
                        });
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
