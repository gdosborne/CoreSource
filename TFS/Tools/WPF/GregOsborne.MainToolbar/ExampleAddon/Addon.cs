// <copyright file="Addon.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/13/2020</date>

namespace ExampleAddon {
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using GregOsborne.Application.Media;
    using GregOsborne.MVVMFramework;
    using Toolbar.Controller;

    public class Addon : ApplicationAddon {
        private DelegateCommand displayAddonCommand = default;
        public Addon() {
            AddonName = "Example Addon";
        }

        public DelegateCommand DisplayAddonCommand => this.displayAddonCommand ?? (this.displayAddonCommand = new DelegateCommand(DisplayAddon, ValidateDisplayAddonState));

        public override void CloseAddon() {
            if (MainWindow != null) {
                MainWindow.Close();
            }
        }

        public override void TriggerControlAddition() {
            var imgSource = "pack://application:,,,/Toolbar.Images;component/Images/Small/Alarm.png".GetImageSourceAbsolute();
            var btn = RequestButton(imgSource, DisplayAddonCommand, AddonName);
            if (btn != null) {
                PlaceableControls.Add(new PlaceableControlDefinition {
                    Control = btn,
                    HasSeparatorBefore = true
                });
            }
            if (MainWindow == null) {
                MainWindow = new MainWindow();
            }

            var cmboBox = RequestComboBox(AddonName, 125, ((MainWindow)MainWindow).View.WeekDays);
            cmboBox.Name = $"{AddonName.Replace(" ", "_")}_WeekdaysComboBox";
            if (cmboBox != null) {
                PlaceableControls.Add(new PlaceableControlDefinition {
                    Control = cmboBox,
                    HasSeparatorBefore = false,
                    HasSeparatorAfter = false
                });
                GregOsborne.Application.Windows.Controls.Extensions.BindElement(cmboBox, ComboBox.ItemsSourceProperty, ((MainWindow)MainWindow).View.WeekDays);
                GregOsborne.Application.Windows.Controls.Extensions.BindElement(cmboBox, ComboBox.SelectedItemProperty, ((MainWindow)MainWindow).View.SelectedDayOfTheWeek);
            }
        }

        private void DisplayAddon(object state) {
            if (MainWindow == null) {
                MainWindow = new MainWindow();
                View = MainWindow.GetView<MainWindowView>();
                MainWindow.Closed += MainWindow_Closed;
            } else {
                MainWindow.Focus();
            }
            MainWindow.Show();
            ((MainWindow)MainWindow).View.WindowTitle = $"{AddonName} - Main Window";
            //((MainWindowView)View).WindowTitle = $"{AddonName} - Main Window";
        }

        private void MainWindow_Closed(object sender, EventArgs e) => MainWindow = null;

        private bool ValidateDisplayAddonState(object state) => true;

        public override Window MainWindow {
            get;
            protected set;
        }

        public override ViewModelBase View {
            get;
            protected set;
        }
    }
}
