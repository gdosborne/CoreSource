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
	public partial class SideButton : UserControl
	{
		public SideButton()
		{
			InitializeComponent();
		}
		private void SetProperties()
		{
			TheBorder.Background = Background;
			TheImage.Width = ImageWidth;
			TheImage.Height = ImageHeight;
			TheImage.Source = Source;
			TheTextBlock.Text = Text;
			TheTextBlock.Foreground = Foreground;
		}
		public new Brush Foreground
		{
			get { return (Brush)GetValue(ForegroundProperty); }
			set { SetValue(ForegroundProperty, value); }
		}
		public Brush MouseOverBrush
		{
			get { return (Brush)GetValue(MouseOverBrushProperty); }
			set { SetValue(MouseOverBrushProperty, value); }
		}
		public Brush MouseDownBrush
		{
			get { return (Brush)GetValue(MouseDownBrushProperty); }
			set { SetValue(MouseDownBrushProperty, value); }
		}
		public new Brush Background
		{
			get { return (Brush)GetValue(BackgroundProperty); }
			set { SetValue(BackgroundProperty, value); }
		}
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
		public double ImageWidth
		{
			get { return (double)GetValue(ImageWidthProperty); }
			set { SetValue(ImageWidthProperty, value); }
		}
		public double ImageHeight
		{
			get { return (double)GetValue(ImageHeightProperty); }
			set { SetValue(ImageHeightProperty, value); }
		}
		public ImageSource Source
		{
			get { return (ImageSource)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SideButton), new PropertyMetadata(string.Empty, onTextChanged));
		public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(SideButton), new PropertyMetadata(SystemColors.ControlTextBrush, onForegroundChanged));
		public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(SideButton), new PropertyMetadata(SystemColors.ControlBrush, onBackgroundChanged));
		public static readonly DependencyProperty MouseDownBrushProperty = DependencyProperty.Register("MouseDownBrush", typeof(Brush), typeof(SideButton), new PropertyMetadata(SystemColors.ControlBrush, onMouseDownBrushChanged));
		public static readonly DependencyProperty MouseOverBrushProperty = DependencyProperty.Register("MouseOverBrush", typeof(Brush), typeof(SideButton), new PropertyMetadata(SystemColors.ControlBrush, onMouseOverBrushChanged));
		public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register("ImageWidth", typeof(double), typeof(SideButton), new PropertyMetadata(16.0, onImageWidthChanged));
		public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register("ImageHeight", typeof(double), typeof(SideButton), new PropertyMetadata(16.0, onImageHeightChanged));
		public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(SideButton), new PropertyMetadata(null, onSourceChanged));
		private static void onForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SideButton obj = d as SideButton;
			if (obj == null)
				return;
			obj.Foreground = (Brush)e.NewValue;
			obj.SetProperties();
		}
		private static void onBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SideButton obj = d as SideButton;
			if (obj == null)
				return;
			obj.Background = (Brush)e.NewValue;
			obj.SetProperties();
		}
		private static void onMouseOverBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SideButton obj = d as SideButton;
			if (obj == null)
				return;
			obj.MouseOverBrush = (Brush)e.NewValue;
			obj.SetProperties();
		}
		private static void onMouseDownBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SideButton obj = d as SideButton;
			if (obj == null)
				return;
			obj.MouseDownBrush = (Brush)e.NewValue;
			obj.SetProperties();
		}
		private static void onTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SideButton obj = d as SideButton;
			if (obj == null)
				return;
			obj.Text = (string)e.NewValue;
			obj.SetProperties();
		}
		private static void onImageWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SideButton obj = d as SideButton;
			if (obj == null)
				return;
			obj.ImageWidth = (double)e.NewValue;
			obj.SetProperties();
		}
		private static void onImageHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SideButton obj = d as SideButton;
			if (obj == null)
				return;
			obj.ImageHeight = (double)e.NewValue;
			obj.SetProperties();
		}
		private static void onSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SideButton obj = d as SideButton;
			if (obj == null)
				return;
			obj.Source = (ImageSource)e.NewValue;
			obj.SetProperties();
		}
		private void Button_MouseEnter(object sender, MouseEventArgs e)
		{
			TheBorder.Background = MouseOverBrush;
		}
		private void Button_MouseLeave(object sender, MouseEventArgs e)
		{
			TheBorder.Background = Background;
		}
		private void Button_MouseDown(object sender, MouseButtonEventArgs e)
		{
			TheBorder.Background = MouseDownBrush;
		}
		private void Button_MouseUp(object sender, MouseButtonEventArgs e)
		{
			TheBorder.Background = MouseOverBrush;
		}
	}
}
