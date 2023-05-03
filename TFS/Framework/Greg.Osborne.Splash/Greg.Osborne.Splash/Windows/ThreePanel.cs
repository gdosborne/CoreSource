namespace Greg.Osborne.Splash.Windows {
    using System;
    using System.Windows;
    using Greg.Osborne.Splash.LocalWindows;
    using GregOsborne.Application.Primitives;

    public class ThreePanel : ISplashWindow {
        public ThreePanel() {
            WindowHelper.LoadCommonProperties();
            if (WindowHelper.CommonProperties.ContainsKey("CornerRadius")) {
                var cvtr = new CornerRadiusConverter();
                CornerRadius = (CornerRadius)cvtr.ConvertFromString(WindowHelper.CommonProperties["CornerRadius"]);
            }
        }

        public void WriteStatus(string value) {

        }

        public CornerRadius CornerRadius { get; set; } = new CornerRadius(10);

        private ThreePanelWindow Initialize() {
            var win = new ThreePanelWindow();
            win.View.CornerRadius = CornerRadius;
            return win;
        }

        public DisplayResult Show() {
            var result = new DisplayResult();
            var win = Initialize();
            result.WindowResult = win.ShowDialog();
            return result;
        }
        public void ShowAsync() {
            var win = Initialize();
            win.Closed += Win_Closed;
            win.Show();
        }

        private void Win_Closed(object sender, EventArgs e) {
            WindowClosed?.Invoke(this, e);
        }

        public event EventHandler WindowClosed;
    }
}
