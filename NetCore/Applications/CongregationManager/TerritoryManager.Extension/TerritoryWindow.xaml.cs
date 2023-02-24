using Common.Application.Primitives;
using Common.Application.Windows;
using CongregationExtension.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TerritoryManager.Extension.ViewModels;

namespace TerritoryManager.Extension {
    public partial class TerritoryWindow : Window {
        public TerritoryWindow() {
            InitializeComponent();

            View.Initialize(App.AppSettings, App.DataManager);
            View.ExecuteUiAction += View_ExecuteUiAction;

            Closing += TerritoryWindow_Closing;
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.SetBounds(View.AppSettings);
        }

        private void TerritoryWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) =>
            this.SaveBounds(View.AppSettings);

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (LocalBase.Actions)Enum.Parse(typeof(LocalBase.Actions), e.CommandToExecute);
            switch (action) {
                case LocalBase.Actions.CloseWindow: {
                        DialogResult = false;
                        break;
                    }
                case LocalBase.Actions.AcceptData: {
                        DialogResult = true;
                        break;
                    }
                case LocalBase.Actions.ShowDoNotCall: {

                        break;
                    }
                case LocalBase.Actions.ShowHistory: {

                        break;
                    }
            }
        }

        public TerritoryWindowViewModel View => DataContext.As<TerritoryWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) => sender.As<TextBox>().SelectAll();

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e) =>
            UpdateColumnsWidth(sender.As<ListView>());

        private void ListView_Loaded(object sender, RoutedEventArgs e) => UpdateColumnsWidth(sender.As<ListView>());

        private void UpdateColumnsWidth(ListView lv) {
            var gv = lv.View.As<GridView>();
            if (lv.ActualWidth == double.NaN || lv.ActualWidth == 0)
                return;
            var otherWidth = 0.0;
            var offset = gv.Columns.Count * 6.75;
            gv.Columns.ToList().ForEach(x => {
                if (x == gv.Columns.Last()) {
                    var val = (lv.ActualWidth >= 0 ? lv.ActualWidth - otherWidth : 0) - offset;
                    x.Width = val >= 0 ? val : 20;
                }
                else
                    otherWidth += x.ActualWidth;
            });
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {

        }
    }
}
