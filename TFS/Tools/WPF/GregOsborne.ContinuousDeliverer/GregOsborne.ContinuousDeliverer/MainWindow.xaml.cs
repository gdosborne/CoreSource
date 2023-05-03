namespace GregOsborne.ContinuousDeliverer {
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Windows;
    using System;
    using System.Windows;

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            Visibility = Visibility.Hidden;
            View.Initialize();
            this.RestorePosition(App.ApplicationName);
        }

        public MainWindowView View => DataContext.As<MainWindowView>();

        protected override void OnSourceInitialized(EventArgs e) => this.HideMinimizeAndMaximizeButtons();

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e) {

        }

        private void OnCloseButtonClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            this.SavePosition(App.ApplicationName);
            myTaskBarIcon.Dispose();
            App.Current.Shutdown();
        }

        private void OnToolTipClick(object sender, System.Windows.Input.MouseButtonEventArgs e) => 
            Visibility = Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
    }
}
