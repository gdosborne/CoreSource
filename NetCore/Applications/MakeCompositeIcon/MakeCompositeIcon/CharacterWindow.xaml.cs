using Common.Application.Primitives;
using Common.Application.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MakeCompositeIcon {
    public partial class CharacterWindow : Window {
        public CharacterWindow() {
            InitializeComponent();
            View.PropertyChanged += View_PropertyChanged;
            Closing += CharacterWindow_Closing;
            View.Initialize();
        }

        private void CharacterWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(CharacterWindow), "Left", RestoreBounds.Left);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(CharacterWindow), "Top", RestoreBounds.Top);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(CharacterWindow), "Width", RestoreBounds.Width);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(CharacterWindow), "Height", RestoreBounds.Height);
        }

        private void View_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "DialogResult")
                DialogResult = View.DialogResult;
        }

        internal CharacterWindowView View => DataContext.As<CharacterWindowView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            this.HideMinimizeAndMaximizeButtons();
            if (App.ThisApp.IsUseLastPositionChecked) {
                Left = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(CharacterWindow), "Left", double.IsInfinity(Left) ? 0 : Left);
                Top = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(CharacterWindow), "Top", double.IsInfinity(Top) ? 0 : Top);
                Width = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(CharacterWindow), "Width", Width);
                Height = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(CharacterWindow), "Height", Height);
            }

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) => sender.As<TextBox>().SelectAll();

        private void ListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (View.SelectedCharacter == null)
                return;

            //this is added to make sure we don't double-click on the scrollbars of the listview
            // for this to work, each glyph textblock must have the tag of "isglyph"
            isScrollViewer = false;
            isGlyph = false;
            var pos = e.GetPosition(this);
            var callback = new HitTestResultCallback(HitTestCallBack);

            VisualTreeHelper.HitTest(this, null, callback, new PointHitTestParameters(pos));
            e.Handled = true;
            if (isGlyph)
                View.DialogResult = true;
        }

        private bool isScrollViewer = false;
        private bool isGlyph = false;

        private HitTestResultBehavior HitTestCallBack(HitTestResult result) {
            if (result.VisualHit.Is<ScrollViewer>()) {
                isScrollViewer = true;
                return HitTestResultBehavior.Stop;
            }
            else if (result.VisualHit.Is<TextBlock>() && ((string)result.VisualHit.As<TextBlock>().Tag) == "isglyph") {
                isGlyph = true;
                return HitTestResultBehavior.Stop;
            }
            return HitTestResultBehavior.Continue;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            sender.As<ListView>().ScrollIntoView(sender.As<ListView>().SelectedItem);
            sender.As<ListView>().Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

        }
    }
}
