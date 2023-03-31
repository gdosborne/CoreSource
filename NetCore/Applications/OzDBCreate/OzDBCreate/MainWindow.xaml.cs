using Common.Application.Primitives;
using Common.Application.Windows;
using Common.Application.Windows.Controls;
using OzDBCreate.ViewModel;
using System;
using System.Windows;

namespace OzDBCreate {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Closing += MainWindow_Closing;

            View.Initialize();
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            App.SaveWindowBounds(this, true);
        }

        public MainWindowView View => DataContext.As<MainWindowView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            MainToolbar.RemoveOverflow();
            App.RestoreWindowBounds(this, true);
        }
    }
}
