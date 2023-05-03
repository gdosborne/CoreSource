using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace DomainationControls
{
	public partial class TextBoxWithLabel : UserControl
	{
		public TextBoxWithLabel()
		{
			InitializeComponent();
		}
		private void SetProperties()
		{
			TheLabel.Content = Label;
			TheTextBox.Text = Text;
			TheLabel.Foreground = LabelForeground;
			TheTextBox.TextWrapping = TextWrapping;
			TheTextBox.AcceptsReturn = AcceptsReturn;
			TheTextBox.AcceptsTab = AcceptsTab;
		}
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
		public object Label
		{
			get { return (object)GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}
		public Brush LabelForeground
		{
			get { return (Brush)GetValue(LabelForegroundProperty); }
			set { SetValue(LabelForegroundProperty, value); }
		}
		public TextWrapping TextWrapping
		{
			get { return (TextWrapping)GetValue(TextWrappingProperty); }
			set { SetValue(TextWrappingProperty, value); }
		}
		public bool AcceptsReturn
		{
			get { return (bool)GetValue(AcceptsReturnProperty); }
			set { SetValue(AcceptsReturnProperty, value); }
		}
		public bool AcceptsTab
		{
			get { return (bool)GetValue(AcceptsTabProperty); }
			set { SetValue(AcceptsTabProperty, value); }
		}
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextBoxWithLabel), new PropertyMetadata(string.Empty, onTextChanged));
		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(object), typeof(TextBoxWithLabel), new PropertyMetadata(null, onLabelChanged));
		public static readonly DependencyProperty LabelForegroundProperty = DependencyProperty.Register("LabelForeground", typeof(Brush), typeof(TextBoxWithLabel), new PropertyMetadata(SystemColors.ControlTextBrush, onLabelForegroundChanged));
		public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(TextBoxWithLabel), new PropertyMetadata(TextWrapping.NoWrap, onTextWrappingChanged));
		public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register("AcceptsReturn", typeof(bool), typeof(TextBoxWithLabel), new PropertyMetadata(false, onAcceptsReturnChanged));
		public static readonly DependencyProperty AcceptsTabProperty = DependencyProperty.Register("AcceptsTab", typeof(bool), typeof(TextBoxWithLabel), new PropertyMetadata(false, onAcceptsTabChanged));
		private static void onTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TextBoxWithLabel obj = d as TextBoxWithLabel;
			if (obj == null)
				return;
			obj.Text = (string)e.NewValue;
			obj.SetProperties();
		}
		private static void onLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TextBoxWithLabel obj = d as TextBoxWithLabel;
			if (obj == null)
				return;
			obj.Label = (object)e.NewValue;
			obj.SetProperties();
		}
		private static void onLabelForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TextBoxWithLabel obj = d as TextBoxWithLabel;
			if (obj == null)
				return;
			obj.LabelForeground = (Brush)e.NewValue;
			obj.SetProperties();
		}
		private static void onTextWrappingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TextBoxWithLabel obj = d as TextBoxWithLabel;
			if (obj == null)
				return;
			obj.TextWrapping = (TextWrapping)e.NewValue;
			obj.SetProperties();
		}
		private static void onAcceptsReturnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TextBoxWithLabel obj = d as TextBoxWithLabel;
			if (obj == null)
				return;
			obj.AcceptsReturn = (bool)e.NewValue;
			obj.SetProperties();
		}
		private static void onAcceptsTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TextBoxWithLabel obj = d as TextBoxWithLabel;
			if (obj == null)
				return;
			obj.AcceptsTab = (bool)e.NewValue;
			obj.SetProperties();
		}
		private void TheTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			Text = (sender as TextBox).Text;
		}
	}
}
