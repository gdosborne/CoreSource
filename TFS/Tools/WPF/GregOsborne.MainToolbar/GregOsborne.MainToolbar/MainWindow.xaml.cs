// <copyright file="MainWindow.xaml.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/13/2020</date>

namespace GregOsborne.MainToolbar {
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    using GregOsborne.Application;
    using GregOsborne.Application.Logging;
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Security;
    using GregOsborne.Application.Windows;
    using GregOsborne.Application.Windows.Controls;
    using GregOsborne.Dialogs;

    public partial class MainWindow : Window {
        private bool isSkipAskExit = false;
        public MainWindow() {
            InitializeComponent();
            View.Initialize();

            var isPositionSaved = AppSession.ApplicationSettings.GetValue("Personal.Settings", "SaveLocation", true);
            if (isPositionSaved) {
                var loc = AppSession.ApplicationSettings.GetValue("Personal.Settings", "MainWindow.Location", new Point(100, 100).ToString());
                if (string.IsNullOrEmpty(loc)) {
                    loc = new Point(100, 100).ToString();
                }

                var location = Point.Parse(loc);
                var width = AppSession.ApplicationSettings.GetValue("Personal.Settings", "MainWindow.Width", 800.0);
                Left = location.X;
                Top = location.Y;
                Width = width;
            }
        }

        public Session AppSession => App.Current.As<App>().ApplicationSession;

        public MainWindowView View => DataContext.As<MainWindowView>();

        protected override void OnSourceInitialized(EventArgs e) => this.HideWindowControls(GregOsborne.Application.Windows.Extension.WindowParts.MaximizeButton);

        private bool? GetCredentials(ref string username, out string password) {
            password = string.Empty;
            var dlg = new CredentialDialog {
                MainInstruction = "Enter Credentials",
                Content = $"Enter your credentials for {AppSession.ApplicationName}",
                ShowSaveCheckBox = true,
                WindowTitle = "Login",
                Target = AppSession.ApplicationName,
                ShowUIForSavedCredentials = true
            };
            var result = dlg.ShowDialog();
            if (!result) {
                return null;
            }
            username = dlg.Credentials.UserName;
            password = dlg.Credentials.Password;
            result = UserAccess.IsUserAuthenticated(username, password);
            if (result && dlg.IsSaveChecked) {
                dlg.ConfirmCredentials(true);
            }
            return result;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            var isAskToExit = AppSession.ApplicationSettings.GetValue("Personal.Settings", "Application.AskToExit", true);
            if (!this.isSkipAskExit && isAskToExit) {
                var td = new TaskDialog {
                    MainInstruction = "Are you sure you want to close this application?",
                    MainIcon = TaskDialogIcon.Information,
                    WindowTitle = View.WindowTitle,
                    AllowDialogCancellation = true,
                    ButtonStyle = TaskDialogButtonStyle.Standard,
                    MinimizeBox = false,
                };
                td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
                td.Buttons.Add(new TaskDialogButton(ButtonType.No));
                var result = td.ShowDialog(this);
                if (result.ButtonType == ButtonType.No) {
                    e.Cancel = true;
                }
            }
            if (App.Current.As<App>().ControllerManager.Addons.Any()) {
                foreach (var addon in App.Current.As<App>().ControllerManager.Addons) {
                    addon.CloseAddon();
                }
            }
            AppSession.ApplicationSettings.AddOrUpdateSetting("Personal.Settings", "MainWindow.Location", RestoreBounds.Location.ToString());
            AppSession.ApplicationSettings.AddOrUpdateSetting("Personal.Settings", "MainWindow.Width", RestoreBounds.Width.ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            var t = new DispatcherTimer {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            t.Tick += (s, ee) => {
                View.UpdateInterface();
                s.As<DispatcherTimer>().Stop();
                var username = default(string);
                View.IsAuthenticated = GetCredentials(ref username, out var password);
                if (!View.IsAuthenticated.HasValue) {
                    AppSession.Logger.LogMessage("Login cancelled", ApplicationLogger.EntryTypes.Information);
                    this.isSkipAskExit = true;
                    App.Current.Shutdown(-99);
                } else if (View.IsAuthenticated.Value) {
                    AppSession.Logger.LogMessage($"Login successful for {username}", ApplicationLogger.EntryTypes.Information);
                } else {
                    AppSession.Logger.LogMessage($"Login failed for {username}", ApplicationLogger.EntryTypes.Warning);
                }
                if (App.Current.As<App>().ControllerManager.Addons.Any()) {
                    foreach (var addon in App.Current.As<App>().ControllerManager.Addons) {
                        if (addon.PlaceableControls.Any()) {
                            var btnStyle = FindResource("toolbarButton").As<Style>();
                            var sepStyle = FindResource("separatorLine").As<Style>();
                            var imgStyle = FindResource("toolbarButtonImage").As<Style>();
                            var cboStyle = FindResource("toolbarComboBox").As<Style>();

                            foreach (var item in addon.PlaceableControls) {
                                if (item.HasSeparatorBefore) {
                                    var sep = Extensions.GetBoundElement<Line>(this.myStackPanel, Line.Y2Property, null);
                                    sep.Style = sepStyle;
                                    this.myStackPanel.Children.Add(sep);
                                }
                                if (item.Control.Is<Button>()) {
                                    if (item.Control.As<Button>().Content.Is<Image>()) {
                                        item.Control.As<Button>().Content.As<Image>().Style = imgStyle;
                                    }

                                    item.Control.As<Button>().Style = btnStyle;
                                } else if (item.Control.Is<ComboBox>()) {
                                    item.Control.As<ComboBox>().Style = cboStyle;
                                }
                                this.myStackPanel.Children.Add(item.Control);
                                if (item.HasSeparatorAfter) {
                                    var sep = Extensions.GetBoundElement<Line>(this.myStackPanel, Line.Y2Property, null);
                                    sep.Style = sepStyle;
                                    this.myStackPanel.Children.Add(sep);
                                }
                            }
                        }
                    }
                }
            };
            t.Start();
        }
    }
}
