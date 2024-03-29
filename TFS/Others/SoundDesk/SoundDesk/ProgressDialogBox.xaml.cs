namespace SoundDesk
{
	using GregOsborne.Application.Windows;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

	public partial class ProgressDialogBox : Window
	{
		#region Public Constructors
		public ProgressDialogBox()
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
		private static void onMaximumChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (ProgressDialogBox)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.MyProgressBar.Maximum = value;
		}
		private static void onMinimumChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (ProgressDialogBox)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.MyProgressBar.Minimum = value;
		}
		private static void onValueChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (ProgressDialogBox)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.MyProgressBar.Value = value;
		}
		#endregion Private Methods

		#region Public Fields
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(ProgressDialogBox), new PropertyMetadata(100.0, onMaximumChanged));
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(ProgressDialogBox), new PropertyMetadata(0.0, onMinimumChanged));
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(ProgressDialogBox), new PropertyMetadata(0.0, onValueChanged));
		#endregion Public Fields

		#region Public Properties
		public double Maximum
		{
			get { return (double)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public double Minimum
		{
			get { return (double)GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public double Value
		{
			get { return (double)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		public ProgressDialogBoxView View { get { return LayoutRoot.GetView<ProgressDialogBoxView>(); } }
		#endregion Public Properties
	}
}
