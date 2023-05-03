namespace SoundDesk.Controls
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;

	public delegate void ValueEnteredHandler(object sender, ValueEnteredEventArgs e);

	public class NumericOnlyTextBox : TextBox
	{
		#region Public Constructors
		public NumericOnlyTextBox()
			: base()
		{
		}
		#endregion Public Constructors

		#region Public Methods
		public static char GetCharFromKey(System.Windows.Input.Key key)
		{
			char ch = ' ';

			int virtualKey = KeyInterop.VirtualKeyFromKey(key);
			byte[] keyboardState = new byte[256];
			GetKeyboardState(keyboardState);

			uint scanCode = MapVirtualKey((uint)virtualKey, MapType.MAPVK_VK_TO_VSC);
			StringBuilder stringBuilder = new StringBuilder(2);

			int result = ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
			switch (result)
			{
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
		#endregion Public Methods

		#region Protected Methods
		protected override void OnGotFocus(RoutedEventArgs e)
		{
			base.OnGotFocus(e);
			SelectAll();
		}
		protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
		{
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
			if (IsDecimalAllowed)
			{
				allowedKeys.Add(System.Windows.Input.Key.OemPeriod);
				allowedKeys.Add(System.Windows.Input.Key.Decimal);
			}
			if (IsNegativeNumbersAllowed)
			{
				allowedKeys.Add(System.Windows.Input.Key.OemMinus);
				allowedKeys.Add(System.Windows.Input.Key.Subtract);
			}
			if (!allowedKeys.Contains(e.Key))
				e.Handled = true;
			else if (e.Key == Key.Enter)
			{
				AddSongNumberToSongsBox(int.Parse(Text));
				Text = string.Empty;
			}
			else
			{
				var value = 0.0;
				var x = Text + GetCharFromKey(e.Key);
				if (double.TryParse(x, out value))
					e.Handled = value < Minimum || value > Maximum;
				else
					e.Handled = true;
			}
		}
		protected override void OnLostFocus(RoutedEventArgs e)
		{
			base.OnLostFocus(e);
			var value = 0;
			if (int.TryParse(Text, out value))
				AddSongNumberToSongsBox(value);
			Text = string.Empty;
		}
		#endregion Protected Methods

		#region Private Methods
		private static void onIsDecimalAllowedChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NumericOnlyTextBox)source;
			if (src == null)
				return;
			var value = (bool)e.NewValue;
		}
		private static void onIsNegativeNumbersAllowedChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NumericOnlyTextBox)source;
			if (src == null)
				return;
			var value = (bool)e.NewValue;
		}
		private static void onMaximumChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NumericOnlyTextBox)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
		}
		private static void onMinimumChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (NumericOnlyTextBox)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
		}
		private void AddSongNumberToSongsBox(int number)
		{
			if (ValueEntered != null)
				ValueEntered(this, new ValueEnteredEventArgs(number));
		}
		#endregion Private Methods

		#region Public Events
		public event ValueEnteredHandler ValueEntered;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty IsDecimalAllowedProperty = DependencyProperty.Register("IsDecimalAllowed", typeof(bool), typeof(NumericOnlyTextBox), new PropertyMetadata(true, onIsDecimalAllowedChanged));
		public static readonly DependencyProperty IsNegativeNumbersAllowedProperty = DependencyProperty.Register("IsNegativeNumbersAllowed", typeof(bool), typeof(NumericOnlyTextBox), new PropertyMetadata(true, onIsNegativeNumbersAllowedChanged));
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(NumericOnlyTextBox), new PropertyMetadata(double.MaxValue, onMaximumChanged));
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(NumericOnlyTextBox), new PropertyMetadata(0.0, onMinimumChanged));
		#endregion Public Fields

		#region Public Enums
		public enum MapType : uint
		{
			MAPVK_VK_TO_VSC = 0x0,
			MAPVK_VSC_TO_VK = 0x1,
			MAPVK_VK_TO_CHAR = 0x2,
			MAPVK_VSC_TO_VK_EX = 0x3,
		}
		#endregion Public Enums

		#region Public Properties
		public bool IsDecimalAllowed
		{
			get { return (bool)GetValue(IsDecimalAllowedProperty); }
			set { SetValue(IsDecimalAllowedProperty, value); }
		}
		public bool IsNegativeNumbersAllowed
		{
			get { return (bool)GetValue(IsNegativeNumbersAllowedProperty); }
			set { SetValue(IsNegativeNumbersAllowedProperty, value); }
		}
		public double Maximum
		{
			get { return (double)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public double Minimum
		{
			get { return (double)GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		#endregion Public Properties
	}

	public class ValueEnteredEventArgs : EventArgs
	{
		#region Public Constructors
		public ValueEnteredEventArgs(double value)
		{
			Value = value;
		}
		#endregion Public Constructors

		#region Public Properties
		public double Value { get; private set; }
		#endregion Public Properties
	}
}
