namespace OSControls
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;

	public partial class ColorPickerAndLabel : UserControl
	{
		#region Public Constructors
		public ColorPickerAndLabel()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Private Methods
		private static void onLabelTextChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (ColorPickerAndLabel)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.TheLabel.Content = value;
		}

		private static void onSelectedColorChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (ColorPickerAndLabel)source;
			if (src == null)
				return;
			var value = (Color?)e.NewValue;
			src.TheColorPicker.SelectedColor = value;
		}

		private void TheColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
		{
			if (SelectedColorChanged != null)
				SelectedColorChanged(this, e);
		}
		#endregion Private Methods

		#region Public Events
		public event RoutedPropertyChangedEventHandler<Color?> SelectedColorChanged;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register("LabelText", typeof(string), typeof(ColorPickerAndLabel), new PropertyMetadata(string.Empty, onLabelTextChanged));

		public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color?), typeof(ColorPickerAndLabel), new PropertyMetadata(null, onSelectedColorChanged));
		#endregion Public Fields

		#region Public Properties
		public string LabelText
		{
			get { return (string)GetValue(LabelTextProperty); }
			set { SetValue(LabelTextProperty, value); }
		}

		public Color? SelectedColor
		{
			get { return (Color?)GetValue(SelectedColorProperty); }
			set { SetValue(SelectedColorProperty, value); }
		}
		#endregion Public Properties
	}
}
