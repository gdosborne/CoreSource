namespace OSControls
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;

	public partial class TimeZoneAndLabel : UserControl
	{
		#region Public Constructors
		public TimeZoneAndLabel()
		{
			InitializeComponent();

			var timeZones = new List<TimeZoneInfo>(TimeZoneInfo.GetSystemTimeZones());
			TheTimeZone.ItemsSource = timeZones;
		}
		#endregion Public Constructors

		#region Private Methods
		private static void onLabelTextChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TimeZoneAndLabel)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.TheLabel.Content = value;
		}

		private static void onSelectedTimeZoneChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TimeZoneAndLabel)source;
			if (src == null)
				return;
			var value = (TimeZoneInfo)e.NewValue;
			src.TheTimeZone.SelectedItem = value;
		}

		private void TheTimeZone_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedTimeZone = (TimeZoneInfo)e.AddedItems[0];
			if (TimeZoneSelectionChanged != null)
				TimeZoneSelectionChanged(this, e);
		}
		#endregion Private Methods

		#region Public Events
		public event SelectionChangedEventHandler TimeZoneSelectionChanged;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register("LabelText", typeof(string), typeof(TimeZoneAndLabel), new PropertyMetadata(string.Empty, onLabelTextChanged));

		public static readonly DependencyProperty SelectedTimeZoneProperty = DependencyProperty.Register("SelectedTimeZone", typeof(TimeZoneInfo), typeof(TimeZoneAndLabel), new PropertyMetadata(null, onSelectedTimeZoneChanged));
		#endregion Public Fields

		#region Public Properties
		public string LabelText
		{
			get { return (string)GetValue(LabelTextProperty); }
			set { SetValue(LabelTextProperty, value); }
		}

		public TimeZoneInfo SelectedTimeZone
		{
			get { return (TimeZoneInfo)GetValue(SelectedTimeZoneProperty); }
			set { SetValue(SelectedTimeZoneProperty, value); }
		}
		#endregion Public Properties
	}
}
