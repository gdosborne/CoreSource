using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace KHSound
{
	public class NumericOnlyTextBox : TextBox
	{
		public NumericOnlyTextBox()
		{
			AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(SelectivelyIgnoreMouseButton), true);
			AddHandler(GotKeyboardFocusEvent, new RoutedEventHandler(SelectAllText), true);
			AddHandler(MouseDoubleClickEvent, new RoutedEventHandler(SelectAllText), true);
		}
		#region AllowDecimalProperty
		public static readonly DependencyProperty AllowDecimalProperty = DependencyProperty.Register("AllowDecimal", typeof(bool), typeof(NumericOnlyTextBox), new UIPropertyMetadata(false, OnAllowDecimalChanged));
		public bool AllowDecimal
		{
			get { return (bool)GetValue(AllowDecimalProperty); }
			set { SetValue(AllowDecimalProperty, value); }
		}
		private static void OnAllowDecimalChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			if(e.NewValue == DependencyProperty.UnsetValue) return;
			var control = source as NumericOnlyTextBox;
			if(control == null) return;
			var value = (bool)e.NewValue;
			control.AllowDecimal = value;
		}
		#endregion AllowDecimalProperty
		protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
		{
			base.OnPreviewTextInput(e);
			e.Text.ToCharArray().ToList().ForEach(x =>
			{
				if(AllowDecimal && x.Equals('.'))
					e.Handled = e.Text.Contains(".");
				else if(!char.IsDigit(x))
					e.Handled = true;
			});
		}
		private static void SelectAllText(object sender, RoutedEventArgs e)
		{
			var textBox = e.OriginalSource as TextBox;
			if(textBox != null)
				textBox.SelectAll();
		}
		private static void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
		{
			DependencyObject parent = e.OriginalSource as UIElement;
			while(parent != null && !(parent is TextBox))
				parent = VisualTreeHelper.GetParent(parent);
			if(parent != null)
			{
				var textBox = (TextBox)parent;
				if(!textBox.IsKeyboardFocusWithin)
				{
					textBox.Focus();
					e.Handled = true;
				}
			}
		}
	}
}
