// <copyright file="Addon.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/16/2020</date>

namespace ApplicationSettings.Addon {
    using System;
    using System.Windows;

    using GregOsborne.Application.Media;
    using GregOsborne.Application.Primitives;
    using GregOsborne.MVVMFramework;

    using Toolbar.Controller;

    public class Addon : ApplicationAddon {
        private DelegateCommand displayAddonCommand = default;
        public Addon() {
            AddonName = "Application Settings";
        }

        public DelegateCommand DisplayAddonCommand => this.displayAddonCommand ?? (this.displayAddonCommand = new DelegateCommand(DisplayAddon, ValidateDisplayAddonState));

        public override Window MainWindow {
            get; protected set;
        }

        public override ViewModelBase View {
            get; protected set;
        }

        public override void CloseAddon() {
            if (MainWindow != null) {
                MainWindow.Close();
            }
        }

        public override void TriggerControlAddition() {
            var imgSource = App.ImageUrls.SmallSettings.GetImageSourceAbsolute();
            var btn = RequestButton(imgSource, DisplayAddonCommand, AddonName);
            if (btn != null) {
                btn.ToolTip = $"Show application settings";
                PlaceableControls.Add(new PlaceableControlDefinition {
                    Control = btn,
                    HasSeparatorBefore = true
                });
            }
            if (MainWindow == null) {
                OpenWindow();
            }
        }

        private void OpenWindow() {
            MainWindow = new MainWindow();
            MainWindow.Closed += MainWindow_Closed;
            MainWindow.GetView<MainWindowView>().Addon = this;
            MainWindow.As<IAddonWindow>().PositionWindow();
        }

        private void DisplayAddon(object state) {
            if (MainWindow == null) {
                OpenWindow();
            } else {
                MainWindow.Focus();
            }
            MainWindow.Show();
            ((MainWindow)MainWindow).View.WindowTitle = $"{AddonName}";
        }

        private void MainWindow_Closed(object sender, EventArgs e) {
            MainWindow.As<IAddonWindow>().SaveWindowPoition();
            MainWindow = null;
        }

        private bool ValidateDisplayAddonState(object state) => true;
    }
}
