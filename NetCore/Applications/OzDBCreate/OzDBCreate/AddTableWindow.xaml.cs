using Common.Application.Primitives;
using Common.Application.Windows;
using OzDBCreate.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OzDBCreate {
    public partial class AddTableWindow : Window {
        public AddTableWindow() {
            InitializeComponent();
            Closing += AddTableWindow_Closing;

            View.Initialize();
            View.PropertyChanged += View_PropertyChanged;
        }

        private void AddTableWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            App.SaveWindowBounds(this, true);
        }

        private void View_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(View.DialogResult)) {
                DialogResult = View.DialogResult;
            }
        }
        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            this.HideMinimizeAndMaximizeButtons();
            if (App.RestoreWindowPositions) {
                App.RestoreWindowBounds(this, true);
            }
        }

        public AddTableWindowView View => DataContext.As<AddTableWindowView>();

        private void TBGotFocus(object sender, RoutedEventArgs e) {
            sender.As<TextBox>().SelectAll();
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e) {
            var listView = sender.As<ListView>();
            var gridView = listView.View.As<GridView>();
            var actualWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
            var otherColumnsWidth = gridView.Columns.Where(x => !x.Header.As<String>().Equals("Name")).Sum(x => x.ActualWidth);
            var remainder = actualWidth - otherColumnsWidth;

            gridView.Columns[0].Width = remainder;
        }
    }
}
