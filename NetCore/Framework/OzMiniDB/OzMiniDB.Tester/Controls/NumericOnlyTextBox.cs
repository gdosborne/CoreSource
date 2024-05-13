using OzFramework.Primitives;

using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OzMiniDB.Builder.Controls {
    public class NumericOnlyTextBox : TextBox {
        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);
        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, MapType uMapType);
        [DllImport("user32.dll")]
        public static extern int ToUnicode(
            uint wVirtKey,
            uint wScanCode,
            byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)]
            StringBuilder pwszBuff,
            int cchBuff,
            uint wFlags);

        public NumericOnlyTextBox() {
            Padding = new Thickness(0, 0, 5, 0);
        }

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e) {
            base.OnKeyDown(e);
            var allowedKeys = new List<System.Windows.Input.Key>
            {
                System.Windows.Input.Key.D0,
                System.Windows.Input.Key.D1,
                System.Windows.Input.Key.D2,
                System.Windows.Input.Key.D3,
                System.Windows.Input.Key.D4,
                System.Windows.Input.Key.D5,
                System.Windows.Input.Key.D6,
                System.Windows.Input.Key.D7,
                System.Windows.Input.Key.D8,
                System.Windows.Input.Key.D9,
                System.Windows.Input.Key.NumPad0,
                System.Windows.Input.Key.NumPad1,
                System.Windows.Input.Key.NumPad2,
                System.Windows.Input.Key.NumPad3,
                System.Windows.Input.Key.NumPad4,
                System.Windows.Input.Key.NumPad5,
                System.Windows.Input.Key.NumPad6,
                System.Windows.Input.Key.NumPad7,
                System.Windows.Input.Key.NumPad8,
                System.Windows.Input.Key.NumPad9,
                System.Windows.Input.Key.Back,
                System.Windows.Input.Key.Delete,
                System.Windows.Input.Key.Left,
                System.Windows.Input.Key.Right,
                System.Windows.Input.Key.Home,
                System.Windows.Input.Key.End,
                System.Windows.Input.Key.Enter,
                System.Windows.Input.Key.Tab
            };
            if (this.IsDecimalAllowed) {
                allowedKeys.Add(System.Windows.Input.Key.OemPeriod);
                allowedKeys.Add(System.Windows.Input.Key.Decimal);
            }
            if (this.IsNegativeNumbersAllowed) {
                allowedKeys.Add(System.Windows.Input.Key.OemMinus);
                allowedKeys.Add(System.Windows.Input.Key.Subtract);
            }
            if (!allowedKeys.Contains(e.Key)) {
                e.Handled = true;
            } else if (e.Key == Key.Enter) {
                //EnterHit(int.Parse(this.Text));
                //this.Text = string.Empty;
            } else {
                var x = this.Text + GetCharFromKey(e.Key);
                if (double.TryParse(x, out var value)) {
                    e.Handled = value < this.Minimum || value > this.Maximum;
                } else {
                    e.Handled = true;
                }
            }
        }

        public static char GetCharFromKey(System.Windows.Input.Key key) {
            var ch = ' ';

            var virtualKey = KeyInterop.VirtualKeyFromKey(key);
            var keyboardState = new byte[256];
            GetKeyboardState(keyboardState);

            var scanCode = MapVirtualKey((uint)virtualKey, MapType.MAPVK_VK_TO_VSC);
            var stringBuilder = new StringBuilder(2);

            var result = ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
            switch (result) {
                case -1:
                    break;
                case 0:
                    break;
                case 1:
                    ch = stringBuilder[0];
                    break;
                default:
                    ch = stringBuilder[0];
                    break;
            }
            return ch;
        }

        protected override void OnTextChanged(TextChangedEventArgs e) {
            var selStart = SelectionStart;
            var selLength = SelectionLength;
            if (!IsDecimalAllowed) {
                if (Text == ".") {
                    Text = "";
                }
                if (Text.ToCharArray().Contains('.')) {
                    if (double.TryParse(Text, out var temp)) {
                        var val = Convert.ToInt32(System.Math.Round(temp, 0));
                        Text = val.ToString();
                    }
                }
            }
            if (!IsNegativeNumbersAllowed) {
                if (Text == "-") {
                    Text = "";
                }
                if (Text.ToCharArray().Contains('-')) {
                    Text = "0";
                }
            }
            if (selLength > 0) {
                SelectAll();
            }
            if(double.TryParse(Text, out var outVal))
                ValueEntered?.Invoke(this, new ValueEnteredEventArgs(outVal));
            else
                ValueEntered?.Invoke(this, new ValueEnteredEventArgs(0.0));
        }

        protected override void OnGotFocus(RoutedEventArgs e) {
            base.OnGotFocus(e);
            SelectAll();
        }

        private static void onIsDecimalAllowedChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (NumericOnlyTextBox)source;
            if (src.IsNull()) {
                return;
            }
            var value = (bool)e.NewValue;
        }
        private static void onIsNegativeNumbersAllowedChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (NumericOnlyTextBox)source;
            if (src.IsNull()) {
                return;
            }
            var value = (bool)e.NewValue;
        }
        private static void onMaximumChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (NumericOnlyTextBox)source;
            if (src.IsNull()) {
                return;
            }
            var value = (double)e.NewValue;
        }
        private static void onMinimumChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (NumericOnlyTextBox)source;
            if (src.IsNull()) {
                return;
            }
            var value = (double)e.NewValue;
        }

        public event ValueEnteredHandler ValueEntered;

        public static readonly DependencyProperty IsDecimalAllowedProperty = DependencyProperty.Register("IsDecimalAllowed", typeof(bool), typeof(NumericOnlyTextBox), new PropertyMetadata(true, onIsDecimalAllowedChanged));
        public static readonly DependencyProperty IsNegativeNumbersAllowedProperty = DependencyProperty.Register("IsNegativeNumbersAllowed", typeof(bool), typeof(NumericOnlyTextBox), new PropertyMetadata(true, onIsNegativeNumbersAllowedChanged));
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(NumericOnlyTextBox), new PropertyMetadata(double.MaxValue, onMaximumChanged));
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(NumericOnlyTextBox), new PropertyMetadata(0.0, onMinimumChanged));

        public enum MapType : uint {
            MAPVK_VK_TO_VSC = 0x0,
            MAPVK_VSC_TO_VK = 0x1,
            MAPVK_VK_TO_CHAR = 0x2,
            MAPVK_VSC_TO_VK_EX = 0x3,
        }

        public bool IsDecimalAllowed {
            get => (bool)GetValue(IsDecimalAllowedProperty);
            set => SetValue(IsDecimalAllowedProperty, value);
        }
        public bool IsNegativeNumbersAllowed {
            get => (bool)GetValue(IsNegativeNumbersAllowedProperty);
            set => SetValue(IsNegativeNumbersAllowedProperty, value);
        }
        public double Maximum {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
        public double Minimum {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }
    }
    public delegate void ValueEnteredHandler(object sender, ValueEnteredEventArgs e);
    public class ValueEnteredEventArgs : EventArgs {
        public ValueEnteredEventArgs(double value) => Value = value;

        public double Value {
            get; private set;
        }
    }

}
