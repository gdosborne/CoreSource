namespace GregOsborne.Dialog
{
	using MVVMFramework;
	using GregOsborne.Application.Windows;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Media;

	internal partial class FontDialogBox : Window
	{
		#region Public Constructors
		public FontDialogBox()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
		}
		#endregion Protected Methods

		#region Private Methods
		private static void onCurrentFontFamilyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (FontDialogBox)source;
			if (src == null)
				return;
			var value = (FontFamily)e.NewValue;
			src.View.SelectedFontFamily = value;
		}
		private static void onCurrentFontSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (FontDialogBox)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.View.SelectedFontSize = value;
		}
		private static void onCurrentFontWeightChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (FontDialogBox)source;
			if (src == null)
				return;
			var value = (FontWeight)e.NewValue;
			src.View.SelectedFontWeight = value;
		}
		private static void onSizeVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (FontDialogBox)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.View.SizeVisibility = value;
		}
		private static void onStyleVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (FontDialogBox)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.View.StyleVisibility = value;
		}
		private static void onWeightVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (FontDialogBox)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.View.WeightVisibility = value;
		}
		private void FontDialogBoxView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "SelectedFontFamily":
					FontListBox.ScrollIntoView(View.SelectedFontFamily);
					CurrentFontFamily = View.SelectedFontFamily;
					break;
				case "SelectedFontSize":
					CurrentFontSize = View.SelectedFontSize;
					break;
				case "SelectedFontWeight":
					CurrentFontWeight = View.SelectedFontWeight;
					break;
				case "CurrentFontStyle":
					CurrentFontStyle = View.SelectedFontStyle;
					break;
				case "DialogResult":
					this.DialogResult = View.DialogResult;
					break;
			}
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			View.InitView();
			View.SelectedFontFamily = CurrentFontFamily;
			View.SelectedFontSize = CurrentFontSize;
			View.SelectedFontWeight = CurrentFontWeight;
			View.SelectedFontStyle = CurrentFontStyle;
		}
		#endregion Private Methods

		#region Public Fields
		public static readonly DependencyProperty CurrentFontFamilyProperty = DependencyProperty.Register("CurrentFontFamily", typeof(FontFamily), typeof(FontDialogBox), new PropertyMetadata(null, onCurrentFontFamilyChanged));
		public static readonly DependencyProperty CurrentFontSizeProperty = DependencyProperty.Register("CurrentFontSize", typeof(double), typeof(FontDialogBox), new PropertyMetadata(12.0, onCurrentFontSizeChanged));
		public static readonly DependencyProperty CurrentFontWeightProperty = DependencyProperty.Register("CurrentFontWeight", typeof(FontWeight), typeof(FontDialogBox), new PropertyMetadata(FontWeights.Normal, onCurrentFontWeightChanged));
		public static readonly DependencyProperty SizeVisibilityProperty = DependencyProperty.Register("SizeVisibility", typeof(Visibility), typeof(FontDialogBox), new PropertyMetadata(Visibility.Visible, onSizeVisibilityChanged));
		public static readonly DependencyProperty StyleVisibilityProperty = DependencyProperty.Register("StyleVisibility", typeof(Visibility), typeof(FontDialogBox), new PropertyMetadata(Visibility.Visible, onStyleVisibilityChanged));
		public static readonly DependencyProperty WeightVisibilityProperty = DependencyProperty.Register("WeightVisibility", typeof(Visibility), typeof(FontDialogBox), new PropertyMetadata(Visibility.Visible, onWeightVisibilityChanged));
		#endregion Public Fields

		#region CurrentFontStyle
		public FontStyle CurrentFontStyle
		{
			get { return (FontStyle)GetValue(CurrentFontStyleProperty); }
			set { SetValue(CurrentFontStyleProperty, value); }
		}

		public static readonly DependencyProperty CurrentFontStyleProperty = DependencyProperty.Register("CurrentFontStyle", typeof(FontStyle), typeof(FontDialogBox), new PropertyMetadata(FontStyles.Normal, onCurrentFontStyleChanged));
		private static void onCurrentFontStyleChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (FontDialogBox)source;
			if (src == null)
				return;
			var value = (FontStyle)e.NewValue;
			src.View.SelectedFontStyle = value;
		}
		#endregion

		#region Public Properties
		public FontFamily CurrentFontFamily
		{
			get { return (FontFamily)GetValue(CurrentFontFamilyProperty); }
			set { SetValue(CurrentFontFamilyProperty, value); }
		}
		public double CurrentFontSize
		{
			get { return (double)GetValue(CurrentFontSizeProperty); }
			set { SetValue(CurrentFontSizeProperty, value); }
		}
		public FontWeight CurrentFontWeight
		{
			get { return (FontWeight)GetValue(CurrentFontWeightProperty); }
			set { SetValue(CurrentFontWeightProperty, value); }
		}
		public Visibility SizeVisibility
		{
			get { return (Visibility)GetValue(SizeVisibilityProperty); }
			set { SetValue(SizeVisibilityProperty, value); }
		}
		public Visibility StyleVisibility
		{
			get { return (Visibility)GetValue(StyleVisibilityProperty); }
			set { SetValue(StyleVisibilityProperty, value); }
		}
		public FontDialogBoxView View { get { return LayoutRoot.GetView<FontDialogBoxView>(); } }
		public Visibility WeightVisibility
		{
			get { return (Visibility)GetValue(WeightVisibilityProperty); }
			set { SetValue(WeightVisibilityProperty, value); }
		}
		#endregion Public Properties
	}
}
