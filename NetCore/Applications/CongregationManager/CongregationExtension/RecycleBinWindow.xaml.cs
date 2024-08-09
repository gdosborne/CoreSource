using Common.Primitives;
using Common.Windows;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using CongregationManager.Extensibility;
using System;
using System.Windows;
using System.Windows.Input;

namespace CongregationExtension {
    /// <summary>
    /// Interaction logic for RecycleBinWindow.xaml
    /// </summary>
    public partial class RecycleBinWindow : Window {
        public RecycleBinWindow() {
            InitializeComponent();

            View.Initialize(App.AppSettings, App.DataManager);
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (LocalBase.Actions)Enum.Parse(typeof(LocalBase.Actions), e.CommandToExecute);
            switch (action) {
                case LocalBase.Actions.CloseWindow: {
                        DialogResult = false;
                        break;
                    }
            }
        }

        public RecycleBinWindowViewModel View => DataContext.As<RecycleBinWindowViewModel>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.SetBounds(View.AppSettings, false);

        }

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Window_Closed(object sender, EventArgs e) => this.SaveBounds(View.AppSettings, false);

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if (e.NewValue.Is<RecycleItem>())
                View.SelectedItem = e.NewValue.As<RecycleItem>();
            else if (e.NewValue.Is<RecycleGroup>())
                View.SelectedItem = null;
        }
    }
}
