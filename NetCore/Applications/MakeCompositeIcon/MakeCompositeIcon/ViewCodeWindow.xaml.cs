using Common.Application.Primitives;
using Common.Application.Windows;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MakeCompositeIcon {
    public partial class ViewCodeWindow : Window {
        public ViewCodeWindow() {
            InitializeComponent();
            View.PropertyChanged += View_PropertyChanged;
            Closing += ViewCodeWindow_Closing;

            if (App.ThisApp.IsUseLastPositionChecked) {
                Left = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(ViewCodeWindow), "Left", double.IsInfinity(Left) ? 0 : Left);
                Top = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(ViewCodeWindow), "Top", double.IsInfinity(Top) ? 0 : Top);
                Width = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(ViewCodeWindow), "Width", Width);
                Height = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(ViewCodeWindow), "Height", Height);
            }

            View.Initialize();
        }

        private void ViewCodeWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(ViewCodeWindow), "Left", RestoreBounds.Left);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(ViewCodeWindow), "Top", RestoreBounds.Top);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(ViewCodeWindow), "Width", RestoreBounds.Width);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(ViewCodeWindow), "Height", RestoreBounds.Height);
        }

        private void View_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "DialogResult")
                DialogResult = View.DialogResult;
        }

        internal ViewCodeWindowView View => DataContext.As<ViewCodeWindowView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            this.HideControlBox();
            this.HideMinimizeAndMaximizeButtons();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) => sender.As<TextBox>().SelectAll();
    }
}
