using System.Windows;
using GregOsborne.Dialogs;

namespace Toolbar.Controller {
    public partial class ToolbarApp : System.Windows.Application {
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
                Content = "You are running an addon (extension) as an executable. You should understand that some " +
                    "features of the addon may not be available.\n\nContinue?"
            };
            td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
            td.Buttons.Add(new TaskDialogButton(ButtonType.No));
            var result = td.ShowDialog();
            if (result.ButtonType == ButtonType.No) {
                IsShutdownRequired = true;
            }

        }
        public bool IsShutdownRequired { get; set; } = false;

        public static class ImageUrls {
            private static readonly string packStart = "pack://application:,,,/Toolbar.Images;component/Images/";
            private static readonly string fNameSmall = "Small/";
            private static readonly string fNameLarge = "Large/";
            private static readonly string fNameXLarge = "Large/";
            public static string SmallSettings => $"{packStart}{fNameSmall}Settings.png";
            public static string LargeSettings => $"{packStart}{fNameLarge}Settings.png";
            public static string XLargeSettings => $"{packStart}{fNameXLarge}Settings.png";

            public static string SmallHelp => $"{packStart}{fNameSmall}Help.png";
            public static string LargeHelp => $"{packStart}{fNameLarge}Help.png";
            public static string XLargeHelp => $"{packStart}{fNameXLarge}Help.png";

            public static string SmallAlarm => $"{packStart}{fNameSmall}Alarm.png";
            public static string LargeAlarm => $"{packStart}{fNameLarge}Alarm.png";
            public static string XLargeAlarm => $"{packStart}{fNameXLarge}Alarm.png";
        }
    }
}
