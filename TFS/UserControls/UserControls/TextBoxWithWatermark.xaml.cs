using System.Windows;
using System.Windows.Controls;

namespace UserControls {
    public partial class TextBoxWithWatermark : UserControl {
        public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register("TextBox", typeof(TextBox), typeof(TextBoxWithWatermark), new FrameworkPropertyMetadata(null, OnTextBoxPropertyChanged, CoerceTextBoxProperty));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextBoxWithWatermark), new FrameworkPropertyMetadata(null, OnTextPropertyChanged, CoerceTextProperty));
        public static readonly DependencyProperty TurnOffWatermarkProperty = DependencyProperty.Register("TurnOffWatermark", typeof(TurnOffWatermarkEvents), typeof(TextBoxWithWatermark), new FrameworkPropertyMetadata(TurnOffWatermarkEvents.OnTextExists, OnTurnOffWatermarkPropertyChanged, CoerceTurnOffWatermarkProperty));
        public static readonly DependencyProperty WatermarkTextProperty = DependencyProperty.Register("WatermarkText", typeof(string), typeof(TextBoxWithWatermark), new FrameworkPropertyMetadata("Type here...", OnWatermarkTextPropertyChanged, CoerceWatermarkTextProperty));

        public TextBoxWithWatermark() {
            this.InitializeComponent();
            this.SetValue(TextBoxWithWatermark.TextBoxProperty, this._textBox);
        }

        public enum TurnOffWatermarkEvents {
            OnTextExists,
            OnFocus
        }

        public string Text {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public TextBox TextBox {
            get => (TextBox)this.GetValue(TextBoxProperty);
            set => this.SetValue(TextBoxProperty, value);
        }

        public TurnOffWatermarkEvents TurnOffWatermark {
            get => (TurnOffWatermarkEvents)this.GetValue(TurnOffWatermarkProperty);
            set => this.SetValue(TurnOffWatermarkProperty, value);
        }

        public string WatermarkText {
            get => (string)this.GetValue(WatermarkTextProperty);
            set => this.SetValue(WatermarkTextProperty, value);
        }

        private static object CoerceTextBoxProperty(DependencyObject d, object value) {
            var val = (TextBox)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceTextProperty(DependencyObject d, object value) {
            var val = (string)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceTurnOffWatermarkProperty(DependencyObject d, object value) {
            var val = (TurnOffWatermarkEvents)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceWatermarkTextProperty(DependencyObject d, object value) {
            var val = (string)value;
            //coerce value here if necessary
            return val;
        }

        private static void OnTextBoxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TextBoxWithWatermark)d;
            var val = (TextBox)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TextBoxWithWatermark)d;
            var val = (string)e.NewValue;
            obj._textBox.Text = val;
            obj.ProcessWatermark();
        }

        private static void OnTurnOffWatermarkPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TextBoxWithWatermark)d;
            var val = (TurnOffWatermarkEvents)e.NewValue;
            obj.ProcessWatermark();
        }

        private static void OnWatermarkTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TextBoxWithWatermark)d;
            var val = (string)e.NewValue;
            obj._textBlock.Text = val;
            obj.ProcessWatermark();
        }

        private void ProcessWatermark() {
        }
    }
}
