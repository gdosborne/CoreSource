// <copyright file="App.xaml.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/13/2020</date>

namespace ExampleAddon {
    using System.Windows;
    using GregOsborne.Dialogs;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application {
        protected override void OnStartup(StartupEventArgs e) {
#if !DEBUG
            App.Current.Shutdown();
            return;
#endif
            var td = new TaskDialog {
                MainInstruction = "Addon running as executable",
                MainIcon = TaskDialogIcon.Information,
                WindowTitle = "Addon as exe",
                AllowDialogCancellation = true,
                ButtonStyle = TaskDialogButtonStyle.Standard,
                MinimizeBox = false,
                Content = "You are running an addon (extension) as an executable. You should understand that some features of the addon may not be available.\n\nContinue?"
            };
            td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
            td.Buttons.Add(new TaskDialogButton(ButtonType.No));
            var result = td.ShowDialog();
            if (result.ButtonType == ButtonType.No) {
                App.Current.Shutdown();
            }
        }
    }
}
