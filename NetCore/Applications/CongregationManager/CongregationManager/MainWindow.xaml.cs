﻿using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationManager.Extensibility;
using CongregationManager.ViewModels;
using System;
using System.Windows;
using static Common.Applicationn.Logging.ApplicationLogger;

namespace CongregationManager {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            this.SetBounds(App.ApplicationSession.ApplicationSettings);
            App.LogMessage("Opening main window", EntryTypes.Information);


            Closing += MainWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
            View.Initialize();

        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (MainWindowViewModel.Actions)Enum.Parse(typeof(MainWindowViewModel.Actions), e.CommandToExecute);
            switch (action) {
                case MainWindowViewModel.Actions.ViewLogs: {
                        var win = new LogReaderWindow {
                            Owner = this
                        };
                        win.ShowDialog();
                        break;
                    }
                case MainWindowViewModel.Actions.PanelAdded: {
                        if (MainTabControl.Items.Count > 0) {
                            MainTabControl.SelectedItem = MainTabControl.Items[MainTabControl.Items.Count - 1];
                            MainTabControl.Focus();
                        }
                        break;
                    }
                case MainWindowViewModel.Actions.CloseWindow:
                    Close();
                    break;
                case MainWindowViewModel.Actions.Minimize:
                    WindowState = WindowState.Minimized;
                    break;
                case MainWindowViewModel.Actions.Maximize:
                    WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
                    break;
                case MainWindowViewModel.Actions.ManageExtensions: {
                        var win = new ExtensionManagerWindow {
                            Owner = this
                        };
                        win.ShowDialog();
                        break;
                    }
                default:
                    break;
            }
        }

        public void SaveExtensionData(object sender, SaveExtensionDataEventArgs e) {

        }

        public MainWindowViewModel View =>
            DataContext.As<MainWindowViewModel>();

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            this.SaveBounds(App.ApplicationSession.ApplicationSettings);
            App.LogMessage("Closing main window", EntryTypes.Information);
        }

        internal void RetrieveResources(object sender, RetrieveResourcesEventArgs e) =>
            e.Dictionary = myResourceDictionary;

        //private ResourceDictionary myResourceDictionary => new ResourceDictionary {
        //    Source = new Uri("/CongregationManager;component/Resources/MainTheme.xaml", UriKind.RelativeOrAbsolute)
        //};

        private ResourceDictionary myResourceDictionary => App.Current.Resources;

        public void InitializeExtension(ExtensionBase ext) {
            ext.SetUIControls(MainPageToolbar, TopMenu, App.Current.Resources);

            ext.RetrieveResources += RetrieveResources;
            ext.Initialize(App.DataFolder, App.TempFolder,
                App.ApplicationSession.ApplicationSettings,
                App.ApplicationSession.Logger,
                App.DataManager);

            View.Panels.Add(ext.Panel);
        }

        private void TitlebarBorder_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) =>
            DragMove();
    }
}
