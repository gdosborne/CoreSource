namespace OSControls
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;

	public partial class Moon : UserControl
	{
		#region Public Constructors
		public Moon()
		{
			InitializeComponent();
			CurrentDate = DateTime.Now;
		}
		#endregion Public Constructors

		#region Private Methods
		private static void onCurrentDateChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (Moon)source;
			if (src == null)
				return;
			var value = (DateTime)e.NewValue;
			if (DesignerProperties.GetIsInDesignMode(src))
				return;
			if (!src._LastMoonDate.HasValue || src._LastMoonDate.Value.Date != value.Date)
			{
				src.Phase = value.MoonPhaseInt(30);
				src._LastMoonDate = DateTime.Now;
			}
		}

		private static void onPhaseChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (Moon)source;
			if (src == null)
				return;
			if (DesignerProperties.GetIsInDesignMode(src))
				return;
			var value = (int)e.NewValue;
			src.MoonImage.Source = src.GetImageSource(value);
		}

		private string GetImagePack(int number)
		{
			return GetImagePack(string.Format("x-{0}.png", number));
		}

		private string GetImagePack(string fileName)
		{
			return @"pack://application:,,,/OSControls;component/Themes/Moon/" + fileName;
		}

		private ImageSource GetImageSource(string uriString)
		{
			return GetImageSource(new Uri(uriString, UriKind.Absolute));
		}

		private ImageSource GetImageSource(Uri uri)
		{
			return new BitmapImage(uri);
		}

		private ImageSource GetImageSource(int number)
		{
			return new BitmapImage(new Uri(GetImagePack(number)));
		}
		#endregion Private Methods

		#region Public Fields
		public static readonly DependencyProperty CurrentDateProperty = DependencyProperty.Register("CurrentDate", typeof(DateTime), typeof(Moon), new PropertyMetadata(DateTime.Now, onCurrentDateChanged));

		public static readonly DependencyProperty PhaseProperty = DependencyProperty.Register("Phase", typeof(int), typeof(Moon), new PropertyMetadata(0, onPhaseChanged));
		#endregion Public Fields

		#region Private Fields
		private DateTime? _LastMoonDate = null;
		#endregion Private Fields

		#region Public Properties
		public DateTime CurrentDate
		{
			get { return (DateTime)GetValue(CurrentDateProperty); }
			set { SetValue(CurrentDateProperty, value); }
		}

		public int Phase
		{
			get { return (int)GetValue(PhaseProperty); }
			set { SetValue(PhaseProperty, value); }
		}
		#endregion Public Properties
	}
}
