namespace OSControls
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;

	public partial class FontAndLabel : UserControl
	{
		#region Public Constructors
		public FontAndLabel()
		{
			InitializeComponent();

			var fontSizes = new List<double>();
			for (double i = 6; i < 200; i += 2)
			{
				fontSizes.Add(i);
			}
			TheSizeComboBox.ItemsSource = fontSizes;
			var fontWeights = new List<FontWeight>();
			fontWeights.Add(FontWeights.Bold);
			fontWeights.Add(FontWeights.Normal);
			TheWeightComboBox.ItemsSource = fontWeights;

			TheFontComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(x => x.ToString());
		}
		#endregion Public Constructors

		#region Private Methods
		private static void onLabelTextChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (FontAndLabel)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.TheLabel.Content = value;
		}

		private static void onSelectedFontFamilyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (FontAndLabel)source;
			if (src == null)
				return;
			var value = (FontFamily)e.NewValue;
			src.TheFontComboBox.SelectedItem = value;
		}

		private static void onSelectedFontSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (FontAndLabel)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.TheSizeComboBox.SelectedItem = value;
		}

		private static void onSelectedFontWeightChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (FontAndLabel)source;
			if (src == null)
				return;
			var value = (FontWeight)e.NewValue;
			src.TheWeightComboBox.SelectedItem = value;
		}

		private void TheFontComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (FamilySelectionChanged != null)
				FamilySelectionChanged(this, e);
		}

		private void TheSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (SizeSelectionChanged != null)
				SizeSelectionChanged(this, e);
		}

		private void TheWeightComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (WeightSelectionChanged != null)
				WeightSelectionChanged(this, e);
		}
		#endregion Private Methods

		#region Public Events
		public event SelectionChangedEventHandler FamilySelectionChanged;

		public event SelectionChangedEventHandler SizeSelectionChanged;

		public event SelectionChangedEventHandler WeightSelectionChanged;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register("LabelText", typeof(string), typeof(FontAndLabel), new PropertyMetadata(string.Empty, onLabelTextChanged));

		public static readonly DependencyProperty SelectedFontFamilyProperty = DependencyProperty.Register("SelectedFontFamily", typeof(FontFamily), typeof(FontAndLabel), new PropertyMetadata(null, onSelectedFontFamilyChanged));

		public static readonly DependencyProperty SelectedFontSizeProperty = DependencyProperty.Register("SelectedFontSize", typeof(double), typeof(FontAndLabel), new PropertyMetadata(10.0, onSelectedFontSizeChanged));

		public static readonly DependencyProperty SelectedFontWeightProperty = DependencyProperty.Register("SelectedFontWeight", typeof(FontWeight), typeof(FontAndLabel), new PropertyMetadata(FontWeights.Normal, onSelectedFontWeightChanged));
		#endregion Public Fields

		#region Public Properties
		public string LabelText
		{
			get { return (string)GetValue(LabelTextProperty); }
			set { SetValue(LabelTextProperty, value); }
		}

		public FontFamily SelectedFontFamily
		{
			get { return (FontFamily)GetValue(SelectedFontFamilyProperty); }
			set { SetValue(SelectedFontFamilyProperty, value); }
		}

		public double SelectedFontSize
		{
			get { return (double)GetValue(SelectedFontSizeProperty); }
			set { SetValue(SelectedFontSizeProperty, value); }
		}

		public FontWeight SelectedFontWeight
		{
			get { return (FontWeight)GetValue(SelectedFontWeightProperty); }
			set { SetValue(SelectedFontWeightProperty, value); }
		}
		#endregion Public Properties
	}
}
