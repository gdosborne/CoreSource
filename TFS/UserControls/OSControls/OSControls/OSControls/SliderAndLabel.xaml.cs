namespace OSControls
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.Primitives;

	public partial class SliderAndLabel : UserControl
	{
		#region Public Constructors
		public SliderAndLabel()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Private Methods
		private static void onLabelTextChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (SliderAndLabel)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.TheLabel.Content = value;
		}

		private static void onSliderIsSnapToTickEnabledChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (SliderAndLabel)source;
			if (src == null)
				return;
			var value = (bool)e.NewValue;
			src.TheSlider.IsSnapToTickEnabled = value;
		}

		private static void onSliderMaximumChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (SliderAndLabel)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.TheSlider.Maximum = value;
		}

		private static void onSliderMinimumChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (SliderAndLabel)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.TheSlider.Minimum = value;
		}

		private static void onSliderTickFrequencyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (SliderAndLabel)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.TheSlider.TickFrequency = value;
		}

		private static void onSliderTickPlacementChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (SliderAndLabel)source;
			if (src == null)
				return;
			var value = (TickPlacement)e.NewValue;
			src.TheSlider.TickPlacement = value;
		}

		private static void onSliderValueChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (SliderAndLabel)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.TheSlider.Value = value;
			src.TheValue.Content = value.ToString("G4");
		}

		private void TheSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			SliderValue = e.NewValue;
		}
		#endregion Private Methods

		#region Public Fields
		public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register("LabelText", typeof(string), typeof(SliderAndLabel), new PropertyMetadata(string.Empty, onLabelTextChanged));

		public static readonly DependencyProperty SliderIsSnapToTickEnabledProperty = DependencyProperty.Register("SliderIsSnapToTickEnabled", typeof(bool), typeof(SliderAndLabel), new PropertyMetadata(false, onSliderIsSnapToTickEnabledChanged));

		public static readonly DependencyProperty SliderMaximumProperty = DependencyProperty.Register("SliderMaximum", typeof(double), typeof(SliderAndLabel), new PropertyMetadata(0.0, onSliderMaximumChanged));

		public static readonly DependencyProperty SliderMinimumProperty = DependencyProperty.Register("SliderMinimum", typeof(double), typeof(SliderAndLabel), new PropertyMetadata(0.0, onSliderMinimumChanged));

		public static readonly DependencyProperty SliderTickFrequencyProperty = DependencyProperty.Register("SliderTickFrequency", typeof(double), typeof(SliderAndLabel), new PropertyMetadata(1.0, onSliderTickFrequencyChanged));

		public static readonly DependencyProperty SliderTickPlacementProperty = DependencyProperty.Register("SliderTickPlacement", typeof(TickPlacement), typeof(SliderAndLabel), new PropertyMetadata(TickPlacement.None, onSliderTickPlacementChanged));

		public static readonly DependencyProperty SliderValueProperty = DependencyProperty.Register("SliderValue", typeof(double), typeof(SliderAndLabel), new PropertyMetadata(0.0, onSliderValueChanged));
		#endregion Public Fields

		#region Public Properties
		public string LabelText
		{
			get { return (string)GetValue(LabelTextProperty); }
			set { SetValue(LabelTextProperty, value); }
		}

		public bool SliderIsSnapToTickEnabled
		{
			get { return (bool)GetValue(SliderIsSnapToTickEnabledProperty); }
			set { SetValue(SliderIsSnapToTickEnabledProperty, value); }
		}

		public double SliderMaximum
		{
			get { return (double)GetValue(SliderMaximumProperty); }
			set { SetValue(SliderMaximumProperty, value); }
		}

		public double SliderMinimum
		{
			get { return (double)GetValue(SliderMinimumProperty); }
			set { SetValue(SliderMinimumProperty, value); }
		}

		public double SliderTickFrequency
		{
			get { return (double)GetValue(SliderTickFrequencyProperty); }
			set { SetValue(SliderTickFrequencyProperty, value); }
		}

		public TickPlacement SliderTickPlacement
		{
			get { return (TickPlacement)GetValue(SliderTickPlacementProperty); }
			set { SetValue(SliderTickPlacementProperty, value); }
		}

		public double SliderValue
		{
			get { return (double)GetValue(SliderValueProperty); }
			set { SetValue(SliderValueProperty, value); }
		}
		#endregion Public Properties
	}
}
