// <copyright file="MainWindow.xaml.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/16/2020</date>

namespace ApplicationSettings.Addon {
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Forms;

    using GregOsborne.Application.Media;
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Windows;

    using Toolbar.Controller;

    public partial class MainWindow : Window, IAddonWindow {
        public MainWindow() {
            InitializeComponent();
            DataContext = new MainWindowView();
            View.Initialize();
            Icon = App.ImageUrls.XLargeSettings.GetImageSourceAbsolute();
            Closing += MainWindow_Closing;
            MouseDown += MainWindow_MouseDown;
        }

        public MainWindowView View => DataContext.As<MainWindowView>();

        private bool IsInDesignMode => this.IsInDesignMode();

        private bool IsWindowOnPrimaryMonitor => this.IsOnPrimaryMonitor();

        private Screen Screen => this.GetScreen();

        public void PositionWindow() => this.SetWindowBounds(
            GetPart("MainWindow.Position.Left", 100.0),
            GetPart("MainWindow.Position.Top", 100.0),
            GetPart("MainWindow.Position.Width", 800.0),
            GetPart("MainWindow.Position.Height", 400.0));

        public void SaveWindowPoition() => this.SavePosition(View.Addon.Session.ApplicationSettings, View.Addon.AddonName, "MainWindow.Position");

        protected override void OnSourceInitialized(EventArgs e) => this.HideWindowControls(GregOsborne.Application.Windows.Extension.WindowParts.MinimizeButton
                | GregOsborne.Application.Windows.Extension.WindowParts.MaximizeButton);

        private double GetPart(string itemName, double defaultValue) {
            var infChar = '∞';
            var s = View.Addon.Session.ApplicationSettings.GetValueAsString($"{View.Addon.AddonName}", itemName);
            return s.ToCharArray().Contains(infChar) ? defaultValue : Convert.ToDouble(s);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) => SaveWindowPoition();

        private void MainWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            var isOnPM = IsWindowOnPrimaryMonitor;
        }
    }
}
