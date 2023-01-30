﻿using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationManager.Extensibility;
using CongregationManager.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CongregationManager {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            this.SetBounds(App.ApplicationSession.ApplicationSettings);

            Closing += MainWindow_Closing; ;
            View.ExecuteUiAction += View_ExecuteUiAction; ;
            View.Initialize();

            ApplicationData.Extensions.ForEach(x => {
                x.SaveExtensionData += X_SaveExtensionData; ;
                x.AddControlItem += AddControlItem;
                x.RemoveControlItem += RemoveControlItem;
                x.RetrieveResources += RetrieveResources;

                x.Initialize(App.DataFolder, App.TempFolder,
                    App.ApplicationSession.ApplicationSettings,
                    App.ApplicationSession.Logger,
                    App.DataManager);
                View.Panels.Add(x.Panel);
            });
        }

        private void X_SaveExtensionData(object sender, SaveExtensionDataEventArgs e) {
            throw new NotImplementedException();
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (MainWindowViewModel.Actions)Enum.Parse(typeof(MainWindowViewModel.Actions), e.CommandToExecute);
            switch (action) {
                case MainWindowViewModel.Actions.CloseWindow:
                    Close();
                    break;
                case MainWindowViewModel.Actions.Minimize:
                    WindowState = WindowState.Minimized;
                    break;
                case MainWindowViewModel.Actions.Maximize:
                    WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
                    break;
                case MainWindowViewModel.Actions.ManageExtensions:
                    var win = new ExtensionManagerWindow {
                        Owner = this
                    };
                    win.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        public void SaveExtensionData(object sender, SaveExtensionDataEventArgs e) {

        }

        public MainWindowViewModel View => DataContext.As<MainWindowViewModel>();

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            this.SaveBounds(App.ApplicationSession.ApplicationSettings);
        }

        private void RetrieveResources(object sender, RetrieveResourcesEventArgs e) {
            e.Dictionary = myResourceDictionary;
        }

        internal void RemoveControlItem(object sender, RemoveControlItemEventArgs e) {
            foreach (var item in e.Controls) {
                if (item.Is<MenuItem>()) {
                    TopMenu.Items.Remove(item.As<MenuItem>());
                }
                else if (item.Is<Button>()) {
                    MainPageToolbar.Items.Remove(item);
                }
                else if (item.Is<Separator>()) {
                    MainPageToolbar.Items.Remove(item);
                }
            }
        }

        private ResourceDictionary myResourceDictionary => new ResourceDictionary {
            Source = new Uri("/CongregationManager;component/Resources/MainTheme.xaml", UriKind.RelativeOrAbsolute)
        };

        internal void AddControlItem(object sender, Extensibility.AddControlItemEventArgs e) {
            var ext = sender.As<ExtensionBase>();
            switch (e.ControlType) {
                case AddControlItemEventArgs.ControlTypes.TopLevelMenuItem: {
                        var item = new MenuItem {
                            Header = e.Text
                        };
                        TopMenu.Items.Add(item);
                        e.ManagableItem = item;
                        break;
                    }
                case AddControlItemEventArgs.ControlTypes.MenuItem: {
                        if (e.Parent == null || !e.Parent.Is<MenuItem>())
                            return;

                        var parent = e.Parent.As<MenuItem>();
                        var fontIcon = ApplicationData.GetIcon(myResourceDictionary, e.ItemGlyph);
                        var item = new MenuItem {
                            Header = e.Text,
                            Icon = fontIcon,
                            Command = e.Command
                        };
                        parent.Items.Add(item);
                        e.ManagableItem = item;
                        break;
                    }
                case AddControlItemEventArgs.ControlTypes.ToolbarSeparator: {
                        var item = new Separator();
                        MainPageToolbar.Items.Add(item);
                        e.ManagableItem = item;
                        break;
                    }
                case AddControlItemEventArgs.ControlTypes.MenuSeparator: {
                        var item = new Separator();
                        TopMenu.Items.Add(item);
                        e.ManagableItem = item;
                        break;
                    }

                case AddControlItemEventArgs.ControlTypes.ToolbarLabel: {
                        var item = new TextBlock {
                            Text = e.Text,
                            Style = ApplicationData.GetStyle(myResourceDictionary, "ToolbarLabel")
                        };
                        MainPageToolbar.Items.Add(item);
                        e.ManagableItem = item;
                        break;
                    }
                case AddControlItemEventArgs.ControlTypes.ToolbarButton: {
                        var fontIcon = ApplicationData.GetIcon(myResourceDictionary, e.ItemGlyph, "ToolbarIcon");
                        var item = new Button {
                            Content = fontIcon,
                            Command = e.Command,
                            ToolTip = e.Text
                        };
                        MainPageToolbar.Items.Add(item);
                        e.ManagableItem = item;
                        break;
                    }
            }
        }

        private void TitlebarBorder_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            DragMove();
        }
    }
}
