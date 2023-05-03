namespace Greg.Osborne.Splash.LocalWindows {
    using System.Windows;
    using System.Windows.Input;
    using GregOsborne.Application.Primitives;

    internal partial class ThreePanelWindow : Window {
        public ThreePanelWindow() => InitializeComponent();

        private void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e) => Close();

        public ThreePanelWindowView View => DataContext.As<ThreePanelWindowView>();
    }
}
