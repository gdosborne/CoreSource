using Common.Application.Primitives;
using Common.Application.Windows;
using OzDBCreate.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Azure.Core.HttpHeader;

namespace OzDBCreate {
    public partial class DatabasePropertiesWindow : Window {
        public DatabasePropertiesWindow() {
            InitializeComponent();
            Closing += DatabasePropertiesWindow_Closing;

            View.Initialize();
            View.PropertyChanged += View_PropertyChanged;
        }

        private void DatabasePropertiesWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            App.SaveWindowBounds(this, true);
        }

        private void View_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "DialogResult")
                DialogResult = View.DialogResult;
        }

        public DatabasePropertiesWindowView View => DataContext.As<DatabasePropertiesWindowView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            this.HideMinimizeAndMaximizeButtons();
            if (App.RestoreWindowPositions) {
                App.RestoreWindowBounds(this, true);
            }
        }

        private void TBGotFocus(object sender, RoutedEventArgs e) => sender.As<TextBox>().SelectAll();

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            
        }
    }
}
