// -----------------------------------------------------------------------
// Copyright© 2016 Statistcs & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// uRectangle.xaml.cs
//
namespace SNC.OptiRamp.Application.DeveloperEntities.Controls {

	using SNC.OptiRamp.Application.DeveloperEntities.Designer;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;
	using System.Windows.Media;

	public partial class uRectangle : UserControl, IMovable {

		#region Public Constructors
		public uRectangle() {
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Public Methods
		public void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			var element = sender as FrameworkElement;
			anchorPoint = e.GetPosition(null);
			element.CaptureMouse();
			isInDrag = true;
			e.Handled = true;
		}
		public void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			if (isInDrag) {
				var element = sender as FrameworkElement;
				element.ReleaseMouseCapture();
				isInDrag = false;
				e.Handled = true;
				if (LocationChanged != null)
					LocationChanged(this, new LocationChangedEventArgs(anchorPoint));
			}
		}
		public void UserControl_MouseMove(object sender, MouseEventArgs e) {
			if (isInDrag) {
				var element = sender as FrameworkElement;
				currentPoint = e.GetPosition(null);
				transform.X += currentPoint.X - anchorPoint.X;
				transform.Y += (currentPoint.Y - anchorPoint.Y);
				this.RenderTransform = transform;
				anchorPoint = currentPoint;
			}
		}
		#endregion Public Methods

		#region Private Methods
		private static void onCornerRadiusChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (uRectangle)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.MyRectangle.RadiusX = value;
			src.MyRectangle.RadiusY = value;
		}
		private static void onDesignerObjectChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (uRectangle)source;
			if (src == null)
				return;
			var value = (DesignerObject)e.NewValue;
		}
		#endregion Private Methods

		#region Public Events
		public event LocationChangedHandler LocationChanged;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(double), typeof(uRectangle), new PropertyMetadata(0.0, onCornerRadiusChanged));
		public static readonly DependencyProperty DesignerObjectProperty = DependencyProperty.Register("DesignerObject", typeof(DesignerObject), typeof(uRectangle), new PropertyMetadata(null, onDesignerObjectChanged));
		#endregion Public Fields

		#region Private Fields
		private Point anchorPoint;
		private Point currentPoint;
		private bool isInDrag = false;
		private TranslateTransform transform = new TranslateTransform();
		#endregion Private Fields

		#region Public Properties
		public double CornerRadius {
			get {
				return (double)GetValue(CornerRadiusProperty);
			}
			set {
				SetValue(CornerRadiusProperty, value);
			}
		}
		public DesignerObject DesignerObject {
			get {
				return (DesignerObject)GetValue(DesignerObjectProperty);
			}
			set {
				SetValue(DesignerObjectProperty, value);
			}
		}
		#endregion Public Properties
	}
}
