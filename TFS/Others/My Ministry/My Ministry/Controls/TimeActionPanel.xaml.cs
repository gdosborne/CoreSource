namespace MyMinistry.Controls
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Runtime.InteropServices.WindowsRuntime;
	using Views;
	using Windows.Foundation;
	using Windows.Foundation.Collections;
	using Windows.UI.Xaml;
	using Windows.UI.Xaml.Controls;
	using Windows.UI.Xaml.Controls.Primitives;
	using Windows.UI.Xaml.Data;
	using Windows.UI.Xaml.Input;
	using Windows.UI.Xaml.Media;
	using Windows.UI.Xaml.Navigation;

	public sealed partial class TimeActionPanel : UserControl
	{
		#region Public Fields

		public static new readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(TimeActionPanel), new PropertyMetadata(null, onBorderBrushChanged));

		public static new readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(TimeActionPanel), new PropertyMetadata(new Thickness(0), onBorderThicknessChanged));

		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TimeActionPanel), new PropertyMetadata(new CornerRadius(0), onCornerRadiusChanged));

		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TimeActionPanel), new PropertyMetadata("Title", onTitleChanged));

		#endregion Public Fields

		#region Public Constructors

		public TimeActionPanel()
		{
			this.InitializeComponent();
		}

		#endregion Public Constructors

		#region Public Properties

		public new Brush BorderBrush {
			get { return (Brush)GetValue(BorderBrushProperty); }
			set { SetValue(BorderBrushProperty, value); }
		}

		public new Thickness BorderThickness {
			get { return (Thickness)GetValue(BorderThicknessProperty); }
			set { SetValue(BorderThicknessProperty, value); }
		}

		public CornerRadius CornerRadius {
			get { return (CornerRadius)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}

		public string Title {
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public TimeActionPanelView View { get { return this.DataContext as TimeActionPanelView; } }

		#endregion Public Properties

		#region Private Methods

		private static void onBorderBrushChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TimeActionPanel)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.TheBorder.BorderBrush = value;
		}

		private static void onBorderThicknessChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TimeActionPanel)source;
			if (src == null)
				return;
			var value = (Thickness)e.NewValue;
			src.TheBorder.BorderThickness = value;
		}

		private static void onCornerRadiusChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TimeActionPanel)source;
			if (src == null)
				return;
			var value = (CornerRadius)e.NewValue;
			src.TheBorder.CornerRadius = value;
		}

		private static void onTitleChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TimeActionPanel)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.TitleTextBlock.Text = value;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			View.UpdateInterface();
		}

		#endregion Private Methods
	}
}
