// <copyright file="MainWindow.xaml.cs" company="">
// Copyright (c) 2019 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>12/3/2019</date>

namespace GregOsborne.PasswordManager {
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Windows;
    using GregOsborne.PasswordManager.Data;

    public partial class MainWindow : Window {
        private const string slideInAnimationName = "slideInAnimation";
        private const string slideOutAnimationName = "slideOutAnimation";
        private const string leftPart = "Left";
        private const string topPart = "Top";
        private const string widthPart = "Width";
        private const string heightPart = "Height";
        private const string windowStatePart = "WindowState";
        private double maximizeMarginOffset = 5;
        public MainWindow() {
            InitializeComponent();
            Topmost = true;
            BringIntoView();
            var dt = new DispatcherTimer {
                Interval = TimeSpan.FromSeconds(3)
            };
            dt.Tick += delegate (object sender, EventArgs e) {
                sender.As<DispatcherTimer>().Stop();
                Topmost = false;
            };
            dt.Start();
        }

        public MainWindowView View => DataContext.As<MainWindowView>();

        private void TheWindow_Loaded(object sender, RoutedEventArgs e) {
            View.ExecuteUiAction += View_ExecuteUiAction;
            //this.optionsGrid.Width = 0;
            View.Initialize();
            if (View.IsWindowBoundsSaved.HasValue && View.IsWindowBoundsSaved.Value) {
                var settings = Application.Current.As<App>().Session.ApplicationSettings;
                var left = settings.GetValue(GetType().FullName, leftPart, Left);
                var top = settings.GetValue(GetType().FullName, topPart, Top);
                var width = settings.GetValue(GetType().FullName, widthPart, Width);
                var height = settings.GetValue(GetType().FullName, heightPart, Height);
                this.SetBounds(new Point(left, top), new Size(width, height));
                WindowState = settings.GetValue(GetType().FullName, windowStatePart, WindowState);
            }
            this.mainGrid.Background = View.WindowBrush;
        }

        private void TheWindow_SizeChanged(object sender, SizeChangedEventArgs e) {
            if (WindowState == WindowState.Maximized) {
                this.primaryGrid.Margin = new Thickness(this.maximizeMarginOffset);
            } else {
                this.primaryGrid.Margin = new Thickness(0, 0, 0, 0);
            }
        }

        private void View_ExecuteUiAction(object sender, MVVMFramework.ExecuteUiActionEventArgs e) {
            var key = e.UiParameters != null ? e.UiParameters.CommandToExecute : e.CommandToExecute;
            switch (key) {
                case MainWindowView.AddNewSecurityGroupText: {
                    var win = new SecurityGroupWindow {
                        Owner = this,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };
                    win.View.Theme = this.View.Theme;
                    var result = win.ShowDialog();
                    if(result.HasValue && result.Value) {
                        var sg = new SecurityGroup {
                            Description = win.View.Description,
                            Name = win.View.GroupName,
                            ControlBorderBrush = View.ControlBorderBrush,
                            FontSize = View.FontSize,
                            ItemTitleFontSize = View.ItemTitleFontSize,
                            WindowBrush = View.WindowBrush,
                            WindowTextBrush = View.WindowTextBrush
                        };
                        Application.Current.As<App>().SecurityFile.Groups.Add(sg);
                        if (View.SelectedSecurityItem!=null && View.SelectedSecurityItem.Is<SecurityGroup>()) {
                            View.SelectedSecurityItem.As<SecurityGroup>().Groups.Add(sg);
                        } else {
                            View.SecurityItems.Add(sg);
                        }
                        Application.Current.As<App>().SecurityFile.Save();
                    }
                    break;
                }
                case MainWindowView.AddNewSecurityItemText: {

                    break;
                }
                case MainWindowView.ImportFileText: {
                    var ofd = new Dialogs.VistaOpenFileDialog() {
                        AddExtension = false,
                        CheckFileExists = true,
                        CheckPathExists = true,
                        Filter = (string)e.UiParameters["Filter"],
                        InitialDirectory = (string)e.UiParameters["InitialDirectory"],
                        ShowReadOnly = false,
                        Title = (string)e.UiParameters["Title"],
                    };
                    var result = ofd.ShowDialog(this);
                    e.UiParameters["Cancel"] = !result.HasValue || !result.Value;
                    e.UiParameters["PasswordFileName"] = ofd.FileName;
                    break;
                }
                case MainWindowView.ShowMessageText: {
                    var td = new Dialogs.TaskDialog {
                        WindowTitle = (string)e.UiParameters["Title"],
                        MainInstruction = (string)e.UiParameters["MainText"],
                        Content = (string)e.UiParameters["Message"],
                        MainIcon = (Dialogs.TaskDialogIcon)e.UiParameters["Icon"],
                        AllowDialogCancellation = false,
                        ButtonStyle = Dialogs.TaskDialogButtonStyle.Standard,
                        CenterParent = true,
                        MinimizeBox = false,
                    };
                    if (e.UiParameters.TryGetValue("Button1", out var button1)) {
                        td.Buttons.Add((Dialogs.TaskDialogButton)button1);
                    }
                    if (e.UiParameters.TryGetValue("Button2", out var button2)) {
                        td.Buttons.Add((Dialogs.TaskDialogButton)button2);
                    }
                    if (e.UiParameters.TryGetValue("Button3", out var button3)) {
                        td.Buttons.Add((Dialogs.TaskDialogButton)button3);
                    }
                    e.UiParameters["Result"] = td.ShowDialog(this);
                    break;
                }
                case MainWindowView.FocusToNameText: {
                    themeNameTextBox.Focus();
                    break;
                }
                case MainWindowView.ApplyThemeText: {
                    View.Theme.Apply(this);
                    Application.Current.As<App>().SecurityFile.Groups.ToList().ForEach(x => {
                        ThemeSecurityItem(x);
                    });
                    break;
                }
                case MainWindowView.CloseApplicationText: {
                    Close();
                    break;
                }
                case MainWindowView.MinimizeApplicationText: {
                    WindowState = WindowState.Minimized;
                    break;
                }
                case MainWindowView.MaximizeRestoreApplicationText: {
                    WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                    break;
                }
                case MainWindowView.OpenOptionsText:
                case MainWindowView.CloseOptionsText: {
                    var isOpening = this.optionsGrid.ActualWidth == 0;
                    this.mainGrid.IsEnabled = !isOpening;
                    this.mainTitlebar.IsEnabled = !isOpening;

                    var ani = new DoubleAnimation(isOpening ? View.SettingsWidth : 0.0, TimeSpan.FromSeconds(.3));

                    ani.Completed += delegate (object sender1, EventArgs e1) {
                        View.OptionsWidth = View.SettingsWidth;
                    };
                    this.optionsGrid.BeginAnimation(Grid.WidthProperty, ani);

                    break;
                }
            }
        }

        private void ThemeSecurityItem(SecurityItemBase item) {
            item.ControlBorderBrush = View.ControlBorderBrush;
            item.FontSize = View.FontSize;
            item.WindowBrush = View.WindowBrush;
            item.WindowTextBrush = View.WindowTextBrush;
            if (item.Is<SecurityGroup>()) {
                item.As<SecurityGroup>().Groups.ToList().ForEach(x => {
                    ThemeSecurityItem(x);
                });
                item.As<SecurityGroup>().Items.ToList().ForEach(x => {
                    ThemeSecurityItem(x);
                });
            }
        }

        private void TheWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (View.IsWindowBoundsSaved.HasValue && View.IsWindowBoundsSaved.Value) {
                var settings = Application.Current.As<App>().Session.ApplicationSettings;
                settings.AddOrUpdateSetting(GetType().FullName, leftPart, RestoreBounds.Left);
                settings.AddOrUpdateSetting(GetType().FullName, topPart, RestoreBounds.Top);
                settings.AddOrUpdateSetting(GetType().FullName, widthPart, RestoreBounds.Width);
                settings.AddOrUpdateSetting(GetType().FullName, heightPart, RestoreBounds.Height);
                settings.AddOrUpdateSetting(GetType().FullName, windowStatePart, WindowState);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) => sender.As<TextBox>().SelectAll();
    }
}
