using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using GregOsborne.Application.Windows;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Dialog {
    internal partial class FontDialogBox : Window {
        public static readonly DependencyProperty CurrentFontFamilyProperty = DependencyProperty.Register("CurrentFontFamily", typeof(FontFamily), typeof(FontDialogBox), new PropertyMetadata(null, OnCurrentFontFamilyChanged));
        public static readonly DependencyProperty CurrentFontSizeProperty = DependencyProperty.Register("CurrentFontSize", typeof(double), typeof(FontDialogBox), new PropertyMetadata(12.0, OnCurrentFontSizeChanged));
        public static readonly DependencyProperty CurrentFontWeightProperty = DependencyProperty.Register("CurrentFontWeight", typeof(FontWeight), typeof(FontDialogBox), new PropertyMetadata(FontWeights.Normal, OnCurrentFontWeightChanged));
        public static readonly DependencyProperty SizeVisibilityProperty = DependencyProperty.Register("SizeVisibility", typeof(Visibility), typeof(FontDialogBox), new PropertyMetadata(Visibility.Visible, OnSizeVisibilityChanged));
        public static readonly DependencyProperty StyleVisibilityProperty = DependencyProperty.Register("StyleVisibility", typeof(Visibility), typeof(FontDialogBox), new PropertyMetadata(Visibility.Visible, OnStyleVisibilityChanged));
        public static readonly DependencyProperty WeightVisibilityProperty = DependencyProperty.Register("WeightVisibility", typeof(Visibility), typeof(FontDialogBox), new PropertyMetadata(Visibility.Visible, OnWeightVisibilityChanged));
        public static readonly DependencyProperty CurrentFontStyleProperty = DependencyProperty.Register("CurrentFontStyle", typeof(FontStyle), typeof(FontDialogBox), new PropertyMetadata(FontStyles.Normal, OnCurrentFontStyleChanged));

        public FontDialogBox() {
            InitializeComponent();
        }

        public FontStyle CurrentFontStyle {
            get => (FontStyle) GetValue(CurrentFontStyleProperty);
            set => SetValue(CurrentFontStyleProperty, value);
        }

        public FontFamily CurrentFontFamily {
            get => (FontFamily) GetValue(CurrentFontFamilyProperty);
            set => SetValue(CurrentFontFamilyProperty, value);
        }

        public double CurrentFontSize {
            get => (double) GetValue(CurrentFontSizeProperty);
            set => SetValue(CurrentFontSizeProperty, value);
        }

        public FontWeight CurrentFontWeight {
            get => (FontWeight) GetValue(CurrentFontWeightProperty);
            set => SetValue(CurrentFontWeightProperty, value);
        }

        public Visibility SizeVisibility {
            get => (Visibility) GetValue(SizeVisibilityProperty);
            set => SetValue(SizeVisibilityProperty, value);
        }

        public Visibility StyleVisibility {
            get => (Visibility) GetValue(StyleVisibilityProperty);
            set => SetValue(StyleVisibilityProperty, value);
        }

        public FontDialogBoxView View => LayoutRoot.GetView<FontDialogBoxView>();

        public Visibility WeightVisibility {
            get => (Visibility) GetValue(WeightVisibilityProperty);
            set => SetValue(WeightVisibilityProperty, value);
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.HideControlBox();
            this.HideMinimizeAndMaximizeButtons();
        }

        private static void OnCurrentFontFamilyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (FontDialogBox) source;
            if (src == null)
                return;
            var value = (FontFamily) e.NewValue;
            src.View.SelectedFontFamily = value;
        }

        private static void OnCurrentFontSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (FontDialogBox) source;
            if (src == null)
                return;
            var value = (double) e.NewValue;
            src.View.SelectedFontSize = value;
        }

        private static void OnCurrentFontWeightChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (FontDialogBox) source;
            if (src == null)
                return;
            var value = (FontWeight) e.NewValue;
            src.View.SelectedFontWeight = value;
        }

        private static void OnSizeVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (FontDialogBox) source;
            if (src == null)
                return;
            var value = (Visibility) e.NewValue;
            src.View.SizeVisibility = value;
        }

        private static void OnStyleVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (FontDialogBox) source;
            if (src == null)
                return;
            var value = (Visibility) e.NewValue;
            src.View.StyleVisibility = value;
        }

        private static void OnWeightVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (FontDialogBox) source;
            if (src == null)
                return;
            var value = (Visibility) e.NewValue;
            src.View.WeightVisibility = value;
        }

        [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
        private void FontDialogBoxView_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "SelectedFontFamily") {
                FontListBox.ScrollIntoView(View.SelectedFontFamily);
                CurrentFontFamily = View.SelectedFontFamily;
            }
            else if (e.PropertyName == "SelectedFontSize") {
                CurrentFontSize = View.SelectedFontSize;
            }
            else if (e.PropertyName == "SelectedFontWeight") {
                CurrentFontWeight = View.SelectedFontWeight;
            }
            else if (e.PropertyName == "CurrentFontStyle") {
                CurrentFontStyle = View.SelectedFontStyle;
            }
            else if (e.PropertyName == "DialogResult") {
                DialogResult = View.DialogResult;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            View.InitView();
            View.SelectedFontFamily = CurrentFontFamily;
            View.SelectedFontSize = CurrentFontSize;
            View.SelectedFontWeight = CurrentFontWeight;
            View.SelectedFontStyle = CurrentFontStyle;
        }

        private static void OnCurrentFontStyleChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (FontDialogBox) source;
            if (src == null)
                return;
            var value = (FontStyle) e.NewValue;
            src.View.SelectedFontStyle = value;
        }
    }
}